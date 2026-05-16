using Scriptura.Domain.Enums;
using Scriptura.Domain.Primitives;
using Scriptura.Domain.ValueObjects;

namespace Scriptura.Domain.Entities.Catalog
{
    public class Settlement : AggregateRoot
    {
        private readonly List<string> _alternativeNames = [];
        private readonly List<HistoricalDivision> _historicalDivisions = [];

        private Settlement()
        {
        }

        private Settlement(Guid id, string currentName, SettlementType type, ModernDivision? modernAdminDivision, Coordinate? location)
            : base(id)
        {
            CurrentName = currentName;
            Type = type;
            ModernAdminDivision = modernAdminDivision;
            Location = location;
        }

        public string CurrentName { get; private set; }
        public SettlementType Type { get; private set; }
        public ModernDivision? ModernAdminDivision { get; private set; }
        public Coordinate? Location { get; private set; }

        public IReadOnlyList<string> AlternativeNames => _alternativeNames;
        public IReadOnlyList<HistoricalDivision> HistoricalDivisions => _historicalDivisions;

        public static Settlement Create(string currentName, SettlementType type, ModernDivision? modernAdminDivision = null, Coordinate? location = null)
        {
            if(string.IsNullOrWhiteSpace(currentName))
                throw new ArgumentException("Current name cannot be empty.", nameof(currentName));

            return new Settlement(Guid.NewGuid(), currentName, type, modernAdminDivision, location);
        }

        public void AddAlternativeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return;

            if(!_alternativeNames.Contains(name))
                _alternativeNames.Add(name);
        }

        public void AddHistoricalDivision(HistoricalDivision division)
        {
            if (!_historicalDivisions.Contains(division))
                _historicalDivisions.Add(division);
        }

        public void UpdateLocation(Coordinate newLocation)
        {
            Location = newLocation ?? throw new ArgumentNullException(nameof(newLocation));
        }

        public void UpdateModernAdminDivision(ModernDivision newDivision)
        {
            ModernAdminDivision = newDivision ?? throw new ArgumentNullException(nameof(newDivision));
        }

        public void RemoveAlternativeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return;

            _alternativeNames.Remove(name);
        }

        public void RemoveHistoricalDivision(HistoricalDivision division)
        {
            if (division != null)
                _historicalDivisions.Remove(division);
        }
    }
}
