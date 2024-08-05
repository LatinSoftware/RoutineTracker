using FluentResults;

namespace RoutineTracker.Server.Extensions
{
    public static class ResultExtension
    {
        public static T Match<T>(
            this Result resut,
            Func<T> onSuccess,
            Func<List<IError>, T> onFailure) => resut.IsSuccess ? onSuccess() : onFailure(resut.Errors);
    }
}
