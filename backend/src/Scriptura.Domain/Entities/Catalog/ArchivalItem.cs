using Scriptura.Domain.Entities.Digitization;
using Scriptura.Domain.Enums;
using Scriptura.Domain.Primitives;
using Scriptura.Domain.ValueObjects;

namespace Scriptura.Domain.Entities.Catalog
{
    public class ArchivalItem : AggregateRoot
    {
        private readonly List<Guid> _settlementIds = [];
        private readonly List<Scan> _scans = [];

        private ArchivalItem()
        {
        }

        private ArchivalItem(Guid id, ArchivalSignature signature, string title, RecordType type, DateRange? coveredYears)
            : base(id)
        {
            Signature = signature;
            Title = title;
            Type = type;
            CoveredYears = coveredYears;
        }

        public ArchivalSignature Signature { get; private set; }
        public string Title { get; private set; }
        public RecordType Type { get; private set; }
        public DateRange? CoveredYears { get; private set; }

        public IReadOnlyList<Guid> SettlementIds => _settlementIds;
        public IReadOnlyList<Scan> Scans => _scans;

        public static ArchivalItem Create(ArchivalSignature signature, string title, RecordType type, DateRange? coveredYears = null)
        {
            ArgumentNullException.ThrowIfNull(signature);

            if(string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty.", nameof(title));

            return new ArchivalItem(Guid.NewGuid(), signature, title, type, coveredYears);
        }

        public void SetCoverageYears(DateRange dates)
        {
            CoveredYears = dates ?? throw new ArgumentNullException(nameof(dates));
        }

        public void LinkToSettlement(Guid settlementId)
        {
            if(settlementId == Guid.Empty)
                throw new ArgumentException("Settlement ID cannot be empty.");

            if (!_settlementIds.Contains(settlementId))
                _settlementIds.Add(settlementId);
        }

        public void UnlinkSettlement(Guid settlementId)
        {
            if (_settlementIds.Contains(settlementId))
            {
                _settlementIds.Remove(settlementId);
            }
        }

        public void AddScan(Scan scan)
        {
            ArgumentNullException.ThrowIfNull(scan);

            if (scan.ArchivalItemId != Id)
                throw new ArgumentException("This scan belongs to a different Archival Item.");

            _scans.Add(scan);
        }

        public void AddScans(IEnumerable<Scan> scans)
        {
            ArgumentNullException.ThrowIfNull(scans);

            foreach (var scan in scans)
                AddScan(scan);
        }

        public void RemoveScan(Guid scanId)
        {
            var scan = _scans.FirstOrDefault(s => s.Id == scanId);
            if (scan != null)
            {
                _scans.Remove(scan);
            }
        }
    }
}
