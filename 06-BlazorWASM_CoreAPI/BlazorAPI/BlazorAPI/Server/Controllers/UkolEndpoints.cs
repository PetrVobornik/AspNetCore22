using Microsoft.EntityFrameworkCore;
using BlazorAPI.Data;
using BlazorAPI.Shared;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace BlazorAPI.Server.Controllers;

public static class UkolEndpoints
{
    public static void MapUkolEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Ukol").WithTags(nameof(Ukol));

        group.MapGet("/", async (ToDoDbContext db) =>
        {
            return await db.Ukoly.ToListAsync();
        })
        .WithName("GetAllUkols")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Ukol>, NotFound>> (int id, ToDoDbContext db) =>
        {
            return await db.Ukoly.FindAsync(id)
                is Ukol model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetUkolById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<NotFound, NoContent>> (int id, Ukol ukol, ToDoDbContext db) =>
        {
            var foundModel = await db.Ukoly.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (foundModel is null)
            {
                return TypedResults.NotFound();
            }

            ukol.Id = id;
            db.Update(ukol);
            await db.SaveChangesAsync();

            return TypedResults.NoContent();
        })
        .WithName("UpdateUkol")
        .WithOpenApi();

        group.MapPost("/", async (Ukol ukol, ToDoDbContext db) =>
        {
            db.Ukoly.Add(ukol);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Ukol/{ukol.Id}",ukol);
        })
        .WithName("CreateUkol")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok<Ukol>, NotFound>> (int id, ToDoDbContext db) =>
        {
            if (await db.Ukoly.FindAsync(id) is Ukol ukol)
            {
                db.Ukoly.Remove(ukol);
                await db.SaveChangesAsync();
                return TypedResults.Ok(ukol);
            }

            return TypedResults.NotFound();
        })
        .WithName("DeleteUkol")
        .WithOpenApi();
    }
}
