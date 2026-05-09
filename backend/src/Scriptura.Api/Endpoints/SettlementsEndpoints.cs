using Microsoft.AspNetCore.Mvc;
using Scriptura.Api.Contracts;
using Scriptura.Domain.Entities.Catalog;
using Scriptura.Domain.Enums;
using Scriptura.Domain.Repositories;
using Scriptura.Domain.ValueObjects;

namespace Scriptura.Api.Endpoints;

public static class SettlementsEndpoints
{
    public static void MapSettlementEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/settlements")
                       .WithTags("Settlements");

        group.MapPost("/", CreateSettlement);
        group.MapGet("/{id:guid}", GetSettlementById);
        group.MapPost("/{id:guid}/alternative-names", AddAlternativeName);
        group.MapGet("/", GetAllSettlements);
        group.MapPut("/{id:guid}/location", UpdateLocation);
    }

    private static async Task<IResult> CreateSettlement(
        [FromBody] CreateSettlementRequest request,
        [FromServices] ISettlementRepository repository,
        CancellationToken cancellationToken)
    {
        if (!Enum.TryParse<SettlementType>(request.Type, ignoreCase: true, out var settlementType))
        {
            return Results.BadRequest(new { Message = $"Invalid settlement type: '{request.Type}'." });
        }

        ModernDivision? modernDivision = null;
        if (!string.IsNullOrWhiteSpace(request.ModernRegion))
        {
            modernDivision = new ModernDivision(
                request.ModernRegion,
                request.ModernDistrict,
                request.ModernCommunity);
        }

        Coordinate? location = null;

        var settlement = Settlement.Create(
            request.CurrentName,
            settlementType,
            modernDivision,
            location);

        repository.Add(settlement);
        await repository.SaveChangesAsync(cancellationToken);

        var response = new SettlementResponse(
            settlement.Id,
            settlement.CurrentName,
            settlement.Type.ToString(),
            settlement.ModernAdminDivision?.Region,
            settlement.AlternativeNames);

        return Results.Created($"/api/settlements/{settlement.Id}", response);
    }

    private static async Task<IResult> GetSettlementById(
        Guid id,
        [FromServices] ISettlementRepository repository,
        CancellationToken cancellationToken)
    {
        var settlement = await repository.GetByIdAsync(id, cancellationToken);

        if (settlement is null)
            return Results.NotFound(new { Message = $"Settlement with ID {id} not found." });

        var response = new SettlementResponse(
            settlement.Id,
            settlement.CurrentName,
            settlement.Type.ToString(),
            settlement.ModernAdminDivision?.Region,
            settlement.AlternativeNames);

        return Results.Ok(response);
    }

    private static async Task<IResult> GetAllSettlements(
        [FromServices] ISettlementRepository repository,
        CancellationToken cancellationToken)
    {
        var settlements = await repository.GetAllAsync(cancellationToken);

        var response = settlements.Select(s => new SettlementResponse(
            s.Id,
            s.CurrentName,
            s.Type.ToString(),
            s.ModernAdminDivision?.Region,
            s.AlternativeNames));

        return Results.Ok(response);
    }

    private static async Task<IResult> AddAlternativeName(
        Guid id,
        [FromBody] AddAlternativeNameRequest request,
        [FromServices] ISettlementRepository repository,
        CancellationToken cancellationToken)
    {
        var settlement = await repository.GetByIdAsync(id, cancellationToken);

        if (settlement is null)
            return Results.NotFound(new { Message = $"Settlement with ID {id} not found." });

        if (string.IsNullOrWhiteSpace(request.Name))
            return Results.BadRequest(new { Message = "Alternative name cannot be empty." });

        settlement.AddAlternativeName(request.Name);

        await repository.SaveChangesAsync(cancellationToken);

        return Results.Ok(new { Message = "Alternative name added successfully." });
    }

    private static async Task<IResult> UpdateLocation(
        Guid id,
        [FromBody] UpdateLocationRequest request,
        [FromServices] ISettlementRepository repository,
        CancellationToken cancellationToken)
    {
        var settlement = await repository.GetByIdAsync(id, cancellationToken);

        if (settlement is null)
            return Results.NotFound(new { Message = $"Settlement with ID {id} not found." });

        var newLocation = new Coordinate(request.Latitude, request.Longitude);

        settlement.UpdateLocation(newLocation);

        await repository.SaveChangesAsync(cancellationToken);

        return Results.Ok(new { Message = "Settlement location updated successfully." });
    }
}