namespace RoutineTracker.Server.Abstractions
{
    public interface IEndpoint
    {
        void MapEndpoints(IEndpointRouteBuilder app);
    }
}
