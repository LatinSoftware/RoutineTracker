using RoutineTracker.Server.Abstractions;

namespace RoutineTracker.Server.Features.Tests
{
    public class TestEndpoint : IEndpoint
    {
        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            app.MapGet("test", () =>
            {
                return Results.Ok("esto es una prueba");
            }).WithName("test");
        }
    }
}
