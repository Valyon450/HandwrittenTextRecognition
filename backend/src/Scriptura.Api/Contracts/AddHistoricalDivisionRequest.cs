namespace Scriptura.Api.Contracts;

public record AddHistoricalDivisionRequest(
    string? Governorate,
    string? County,
    string? Parish
);