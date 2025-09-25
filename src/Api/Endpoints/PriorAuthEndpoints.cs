using Api.Interfaces;
using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

public static class PriorAuthEndpoints
{
    public static void MapPriorAuthEndpoints(this IEndpointRouteBuilder app)
    {
        
        app.MapGet("/{id:guid}", async ([FromRoute]Guid id, [FromServices]IPriorAuthService service) =>
        {
        var rec = await service.GetByIdAsync(id);
        return rec is null ? Results.NotFound() : Results.Ok(rec);
        });
        
        app.MapPost("/prior-auth", async ([FromBody]PriorAuthRequest req, [FromServices]IPriorAuthService service) =>
        {
            var (IsSuccess, ErrorMessage, RecordId, Status) = await service.CreateAsync(req);
            if (!IsSuccess)
                return Results.BadRequest(ErrorMessage);

            return Results.Created($"/prior-auth/{RecordId}", new { id = RecordId, status = Status });
        });
        
    }
}
