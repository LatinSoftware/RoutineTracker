using AutoMapper;
using FluentResults;
using MediatR;
using RoutineTracker.Server.Abstractions;
using RoutineTracker.Server.Database;
using RoutineTracker.Server.Entities;

namespace RoutineTracker.Server.Features.Exercises
{
    public sealed class ExerciseCreate
    {
        public record CreateCommand : IRequest<Result<ExerciseModel>>
        {
            public required string Name { get; set; }
            public required string Description { get; set; }
            public string? DemostrationUrl { get; set; }
            public List<string> Categories { get; set; } = [];
        }

        public sealed class Handler(ApplicationContext context, IMapper mapper) : IRequestHandler<CreateCommand, Result<ExerciseModel>>
        {
            public async Task<Result<ExerciseModel>> Handle(CreateCommand request, CancellationToken cancellationToken)
            {
                var exercise = mapper.Map<Exercise>(request);
                await context.AddAsync(exercise, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                return Result.Ok(mapper.Map<ExerciseModel>(exercise));
            }
        }

        public class ExerciseMapping : Profile
        {
            public ExerciseMapping()
            {
                CreateMap<CreateCommand, Exercise>()
                    .ConstructUsing(src => Exercise.Create(src.Name, src.Description, src.DemostrationUrl, null))
                    .AfterMap((src, dest) =>
                    {
                        foreach (var item in src.Categories)
                        {
                            dest.AddCategory(item);
                        }
                    });

                CreateMap<Exercise, ExerciseModel>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ExerciseId.Value));

            }
        }

        public sealed class CreateEndpoint : IEndpoint
        {
            public void MapEndpoints(IEndpointRouteBuilder app)
            {
                app.MapPost("exercise", async (CreateCommand command, IMediator mediator, CancellationToken cancellationToken) =>
                {
                    var response = await mediator.Send(command, cancellationToken);
                    return Results.Created($"/exercise/{response.Value.Id}", response.Value);

                })
                    .WithName("ExerciseCreate")
                    .WithTags("Exercise");
            }
        }
    }
}
