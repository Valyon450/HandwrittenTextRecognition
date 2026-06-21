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

        public void UpdateTitle(string newTitle)
        {
            if (string.IsNullOrWhiteSpace(newTitle))
                throw new ArgumentException("Title cannot be empty.", nameof(newTitle));

            Title = newTitle;
        }

        public void UpdateSignature(ArchivalSignature newSignature)
        {
            Signature = newSignature ?? throw new ArgumentNullException(nameof(newSignature));
        }

        public void UpdateType(RecordType newType)
        {
            Type = newType;
        }

        public void RemoveScan(Guid scanId)
        {
            var scan = _scans.FirstOrDefault(s => s.Id == scanId);
            if (scan != null)
            {
                _scans.Remove(scan);
            }
        }

        public void ClearSettlements()
        {
            _settlementIds.Clear();
        }

        public void ClearScans()
        {
            _scans.Clear();
        }

        public bool HasScans => _scans.Any();

        public int ScansCount => _scans.Count;

        public bool HasSettlements => _settlementIds.Any();

        public int SettlementsCount => _settlementIds.Count;

        public string FullSignature => $"{Signature.ArchiveCode} {Signature.Fond}-{Signature.Inventory}-{Signature.ItemNumber}";

        public override string ToString()
        {
            return $"{Title} ({FullSignature})";
        }

        public bool ContainsSettlement(Guid settlementId)
        {
            if (settlementId == Guid.Empty) return false;
            return _settlementIds.Contains(settlementId);
        }

        public void RemoveScan(Scan scan)
        {
            if (scan != null)
                _scans.Remove(scan);
        }

        public void RemoveSettlement(Guid settlementId)
        {
            if (settlementId != Guid.Empty)
                _settlementIds.Remove(settlementId);
        }

        public bool ContainsScan(Scan scan)
        {
            if (scan == null) return false;
            return _scans.Contains(scan);
        }
    }
}
