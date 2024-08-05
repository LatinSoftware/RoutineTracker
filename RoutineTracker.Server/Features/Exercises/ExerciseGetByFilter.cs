using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoutineTracker.Server.Abstractions;
using RoutineTracker.Server.Common;
using RoutineTracker.Server.Database;

namespace RoutineTracker.Server.Features.Exercises
{
    public class ExerciseGetByFilter
    {
        public sealed class Query : PagedRequest, IRequest<Result<PagedResponse<ExerciseModel>>>
        {
            public string? Q { get; set; }
           
        }

        public sealed class Handler(ApplicationContext context, IMapper mapper) : IRequestHandler<Query, Result<PagedResponse<ExerciseModel>>>
        {
            public async Task<Result<PagedResponse<ExerciseModel>>> Handle(Query request, CancellationToken cancellationToken)
            {
               var query = context.Exercises.AsQueryable();

                if (!string.IsNullOrEmpty(request.Q))
                {
                    query = query.Where(x => x.Name.ToLower().Contains(request.Q.ToLower()));
                }

                var count = await query.CountAsync(cancellationToken: cancellationToken);

                if(count == 0)                
                    return PagedResponse<ExerciseModel>.Instance(Enumerable.Empty<ExerciseModel>(), request.PageNumber, request.PageSize, count, "exercise");
                
                query = query.OrderByDescending(x => x.ExerciseId).Skip(request.PageNumber - 1).Take(request.PageSize);
                
                var data = await query.ToListAsync(cancellationToken: cancellationToken);

                return PagedResponse<ExerciseModel>.Instance(mapper.Map<List<ExerciseModel>>(data), request.PageNumber, request.PageSize, count, "exercise");
            }
        }

        public sealed class CreateEndpoint : IEndpoint
        {
            public void MapEndpoints(IEndpointRouteBuilder app)
            {
                app.MapGet("exercise", async (string? q, int? page, int? pageSize,  IMediator mediator) =>
                {

                    var response = await mediator.Send(new Query
                    {
                       Q = q,
                       PageNumber = page??1,
                       PageSize = pageSize??10,
                    });
                    return Results.Ok(response.Value);

                }).WithName("ExerciseFilter")
                .WithTags("Exercise");
            }
        }
    }
}
