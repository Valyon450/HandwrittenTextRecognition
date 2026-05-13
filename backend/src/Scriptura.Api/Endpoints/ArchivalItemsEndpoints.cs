using Microsoft.AspNetCore.Mvc;
using Scriptura.Api.Contracts;
using Scriptura.Domain.Entities.Catalog;
using Scriptura.Domain.Entities.Digitization;
using Scriptura.Domain.Enums;
using Scriptura.Domain.Repositories;
using Scriptura.Domain.ValueObjects;

namespace Scriptura.Api.Endpoints;

public static class ArchivalItemsEndpoints
{
    public static void MapArchivalItemEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/archival-items")
                       .WithTags("Archival Items");

        group.MapPost("/", CreateArchivalItem);
        group.MapGet("/{id:guid}", GetArchivalItemById);
        group.MapPost("/{id:guid}/settlements", LinkSettlement);
        group.MapGet("/", GetAllArchivalItems);
        group.MapPost("/{id:guid}/scans", AddScan);
        group.MapDelete("/{id:guid}", DeleteArchivalItem);
    }

    private static async Task<IResult> CreateArchivalItem(
        [FromBody] CreateArchivalItemRequest request,
        [FromServices] IArchivalItemRepository repository,
        CancellationToken cancellationToken)
    {
        if (!Enum.TryParse<RecordType>(request.Type, ignoreCase: true, out var recordType))
        {
            return Results.BadRequest(new { Message = $"Invalid record type: '{request.Type}'. Allowed values depend on your RecordType enum." });
        }

        var signature = new ArchivalSignature(
            request.ArchiveCode,
            request.Fond,
            request.Inventory,
            request.ItemNumber);

        var item = ArchivalItem.Create(signature, request.Title, recordType);

        repository.Add(item);
        await repository.SaveChangesAsync(cancellationToken);

        var response = new ArchivalItemResponse(
            item.Id,
            item.Title,
            $"{item.Signature.ArchiveCode} {item.Signature.Fond}-{item.Signature.Inventory}-{item.Signature.ItemNumber}",
            item.Type.ToString(),
            item.SettlementIds,
            []);

        return Results.Created($"/api/archival-items/{item.Id}", response);
    }

    private static async Task<IResult> GetArchivalItemById(
        Guid id,
        [FromServices] IArchivalItemRepository repository,
        CancellationToken cancellationToken)
    {
        var item = await repository.GetByIdWithScansAsync(id, cancellationToken);

        if (item is null)
            return Results.NotFound(new { Message = $"Archival item with ID {id} not found." });

        var response = new ArchivalItemResponse(
            item.Id,
            item.Title,
            $"{item.Signature.ArchiveCode} {item.Signature.Fond}-{item.Signature.Inventory}-{item.Signature.ItemNumber}",
            item.Type.ToString(),
            item.SettlementIds,
            item.Scans.Select(s => new ScanResponse(s.OrderNumber, s.SourceUrl)));

        return Results.Ok(response);
    }

    private static async Task<IResult> LinkSettlement(
        Guid id,
        [FromBody] LinkSettlementRequest request,
        [FromServices] IArchivalItemRepository archivalRepository,
        [FromServices] ISettlementRepository settlementRepository,
        CancellationToken cancellationToken)
    {
        var item = await archivalRepository.GetByIdWithScansAsync(id, cancellationToken);
        if (item is null)
            return Results.NotFound(new { Message = $"Archival item with ID {id} not found." });

        var settlement = await settlementRepository.GetByIdAsync(request.SettlementId, cancellationToken);
        if (settlement is null)
            return Results.BadRequest(new { Message = $"Settlement with ID {request.SettlementId} does not exist." });

        try
        {
            item.LinkToSettlement(request.SettlementId);
        }
        catch (ArgumentException ex)
        {
            return Results.BadRequest(new { Message = ex.Message });
        }

        await archivalRepository.SaveChangesAsync(cancellationToken);

        return Results.Ok(new { Message = "Settlement linked successfully." });
    }

    private static async Task<IResult> GetAllArchivalItems(
        [FromQuery] Guid? settlementId,
        [FromServices] IArchivalItemRepository repository,
        CancellationToken cancellationToken)
    {
        var items = await repository.GetAllAsync(settlementId, cancellationToken);

        var response = items.Select(item => new ArchivalItemResponse(
            item.Id,
            item.Title,
            $"{item.Signature.ArchiveCode} {item.Signature.Fond}-{item.Signature.Inventory}-{item.Signature.ItemNumber}",
            item.Type.ToString(),
            item.SettlementIds,            
            item.Scans?.Select(s => new ScanResponse(s.OrderNumber, s.SourceUrl)) ?? []));

        return Results.Ok(response);
    }

    private static async Task<IResult> AddScan(
        Guid id,
        [FromBody] AddScanRequest request,
        [FromServices] IArchivalItemRepository repository,
        CancellationToken cancellationToken)
    {
        var item = await repository.GetByIdWithScansAsync(id, cancellationToken);

        if (item is null)
            return Results.NotFound(new { Message = $"Archival item with ID {id} not found." });

        try
        {
            var scan = Scan.Create(
                archivalItemId: item.Id,
                orderNumber: request.OrderNumber,
                sourceUrl: request.SourceUrl);

            item.AddScan(scan);
        }
        catch (ArgumentException ex)
        {
            return Results.BadRequest(new { Message = ex.Message });
        }

        await repository.SaveChangesAsync(cancellationToken);

        return Results.Ok(new { Message = "Scan added successfully." });
    }

    private static async Task<IResult> DeleteArchivalItem(
        Guid id,
        [FromServices] IArchivalItemRepository repository,
        CancellationToken cancellationToken)
    {
        var item = await repository.GetByIdWithScansAsync(id, cancellationToken);

        if (item is null)
            return Results.NotFound(new { Message = $"Archival item with ID {id} not found." });

        repository.Remove(item);

        await repository.SaveChangesAsync(cancellationToken);

        return Results.NoContent();
    }
}