
using FluentResults;
using RoutineTracker.Server.Entities;

namespace RoutineTracker.Server.Features.Exercises
{
    public static class ExerciseErrors
    {
        public static Error NotFound(ExerciseId id) => new($"The exercise with Id {id.Value} do not exist");
    }
}
