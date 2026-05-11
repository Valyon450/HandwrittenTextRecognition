namespace Scriptura.Api.Contracts;

public record SettlementResponse(
    Guid Id,
    string CurrentName,
    string Type,
    string? ModernRegion,
    IEnumerable<string> AlternativeNames,
    IEnumerable<HistoricalDivisionResponse> HistoricalDivisions
);
