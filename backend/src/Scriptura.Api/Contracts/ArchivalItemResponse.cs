namespace Scriptura.Api.Contracts;

public record ArchivalItemResponse(
    Guid Id,
    string Title,
    string FullSignature,
    string Type,
    IEnumerable<Guid> AlternativeNames
);