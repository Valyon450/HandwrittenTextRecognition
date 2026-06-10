namespace Scriptura.Domain.ValueObjects
{
    public record HistoricalDivision
    {
        public string? Governorate { get; init; }
        public string? County { get; init; }
        public string? Parish { get; init; }

        public HistoricalDivision(string governorate, string county, string parish)
        {
            if (string.IsNullOrWhiteSpace(governorate) &&
                string.IsNullOrWhiteSpace(county) &&
                string.IsNullOrWhiteSpace(parish))
            {
                throw new ArgumentException("At least one level of historical division must be provided.");
            }

            Governorate = governorate;
            County = county;
            Parish = parish;
        }

        public bool IsFullySpecified =>
            !string.IsNullOrWhiteSpace(Governorate) &&
            !string.IsNullOrWhiteSpace(County) &&
            !string.IsNullOrWhiteSpace(Parish);

        public override string ToString()
        {
            return $"{Governorate}, {County}, {Parish}";
        }

        public bool IsEmpty =>
            string.IsNullOrWhiteSpace(Governorate) &&
            string.IsNullOrWhiteSpace(County) &&
            string.IsNullOrWhiteSpace(Parish);
    }
}
