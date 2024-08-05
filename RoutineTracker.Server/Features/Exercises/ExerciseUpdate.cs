using AutoMapper;
using FluentResults;
using MediatR;
using RoutineTracker.Server.Abstractions;
using RoutineTracker.Server.Database;
using RoutineTracker.Server.Entities;
using RoutineTracker.Server.Extensions;
using System.Text.Json.Serialization;

namespace RoutineTracker.Server.Features.Exercises
{
    public class ExerciseUpdate
    {
        public record UpdateCommand : IRequest<Result>
        {
            [JsonIgnore]
            public ExerciseId? Id { get; set; }
            public required string Name { get; set; }
            public required string Description { get; set; }
            public string? DemostrationUrl { get; set; }
            public List<string> Categories { get; set; } = [];
        }

        public sealed class Handler(ApplicationContext context, IMapper mapper) : IRequestHandler<UpdateCommand, Result>
        {
            public async Task<Result> Handle(UpdateCommand request, CancellationToken cancellationToken)
            {
                var exercise = await context.Exercises.FindAsync([request.Id!.Value, cancellationToken], cancellationToken: cancellationToken);
                if (exercise == null)
                    return Result.Fail(ExerciseErrors.NotFound(request.Id));

                exercise = mapper.Map(request, exercise);

                context.Update(exercise);

                await context.SaveChangesAsync(cancellationToken);

                return Result.Ok();

            }
        }

        public sealed class Mapping: Profile
        {
            public Mapping()
            {
                CreateMap<UpdateCommand, Exercise>()
                    .ConstructUsing(src => Exercise.Create(src.Name, src.Description, src.DemostrationUrl, src.Id));
                   
            }
        }

        public sealed class Endpoint : IEndpoint
        {
            public void MapEndpoints(IEndpointRouteBuilder app)
            {
                app.MapPut("exercise/{id}", async (Guid id, UpdateCommand command, IMediator mediator) =>
                {
                    command.Id = new ExerciseId(id);
                    var result = await mediator.Send(command);

                    return result.Match(
                        onSuccess: () => Results.NoContent(),
                        onFailure: errors => Results.NotFound(errors.FirstOrDefault())
                    );
                    
                }).WithName("ExerciseUpdate").WithTags("Exercise");
            }
        }
    }
}
