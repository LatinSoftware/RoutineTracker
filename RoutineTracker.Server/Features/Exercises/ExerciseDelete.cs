using FluentResults;
using MediatR;
using RoutineTracker.Server.Abstractions;
using RoutineTracker.Server.Database;
using RoutineTracker.Server.Entities;
using RoutineTracker.Server.Extensions;

namespace RoutineTracker.Server.Features.Exercises
{
    public sealed class ExerciseDelete
    {
        public record Command(ExerciseId ExerciseId) : IRequest<Result>;
        public sealed class Handler(ApplicationContext context) : IRequestHandler<Command, Result>
        {
            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                var exercise = await context.Exercises.FindAsync([request.ExerciseId], cancellationToken: cancellationToken);

                if (exercise == null) return Result.Fail(ExerciseErrors.NotFound(request.ExerciseId));

                context.Exercises.Remove(exercise);

                await context.SaveChangesAsync(cancellationToken);
                
                return Result.Ok();
            }
        }
        public sealed class Endpoint : IEndpoint
        {
            public void MapEndpoints(IEndpointRouteBuilder app)
            {
                app.MapDelete("exercise/{id}", async (Guid id, IMediator mediator) => 
                {

                    var result = await mediator.Send(new Command(new ExerciseId(id)));

                    return result.Match(
                        onSuccess: () => Results.NoContent(),
                        onFailure: errors => Results.NotFound(errors.FirstOrDefault()));

                }).WithName("ExerciseDelete").WithTags("Exercise");
            }
        }
    }
}
