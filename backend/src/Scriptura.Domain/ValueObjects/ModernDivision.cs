namespace Scriptura.Domain.ValueObjects
{
    public record ModernDivision
    {
        public string Region { get; init; }
        public string District { get; init; }
        public string Community { get; init; }

        public ModernDivision(string region, string district, string community)
        {
            Region = region ?? string.Empty;
            District = district ?? string.Empty;
            Community = community ?? string.Empty;
        }

        public bool IsFullySpecified =>
            !string.IsNullOrWhiteSpace(Region) &&
            !string.IsNullOrWhiteSpace(District) &&
            !string.IsNullOrWhiteSpace(Community);

        public override string ToString()
        {
            return $"{Region}, {District}, {Community}";
        }

        public bool IsEmpty =>
            string.IsNullOrWhiteSpace(Region) &&
            string.IsNullOrWhiteSpace(District) &&
            string.IsNullOrWhiteSpace(Community);
    }
}
