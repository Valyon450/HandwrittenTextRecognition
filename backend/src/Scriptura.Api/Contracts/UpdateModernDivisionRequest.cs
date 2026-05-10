namespace Scriptura.Api.Contracts;

public record UpdateModernDivisionRequest(
    string Region,
    string? District = null,
    string? Community = null
);