namespace Scriptura.Api.Contracts;

public record AddScanRequest(
    int OrderNumber,
    string SourceUrl
);