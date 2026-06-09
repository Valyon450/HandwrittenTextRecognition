using Scriptura.Domain.Primitives;

namespace Scriptura.Domain.Entities.Digitization
{
    public class Scan : Entity
    {
        private Scan()
        {
        }

        private Scan(Guid id, Guid archivalItemId, int orderNumber, string sourceUrl)
            : base(id)
        {
            ArchivalItemId = archivalItemId;
            OrderNumber = orderNumber;
            SourceUrl = sourceUrl;
        }

        public Guid ArchivalItemId { get; private set; }
        public int OrderNumber { get; private set; }
        public string SourceUrl { get; private set; }

        public static Scan Create(Guid archivalItemId, int orderNumber, string sourceUrl)
        {
            if(string.IsNullOrWhiteSpace(sourceUrl))
                throw new ArgumentException("Source URL cannot be empty.", nameof(sourceUrl));

            return new Scan(Guid.NewGuid(), archivalItemId, orderNumber, sourceUrl);
        }

        public void UpdateDetails(int newOrderNumber, string newSourceUrl)
        {
            if (string.IsNullOrWhiteSpace(newSourceUrl))
                throw new ArgumentException("Source URL cannot be empty.", nameof(newSourceUrl));

            OrderNumber = newOrderNumber;
            SourceUrl = newSourceUrl;
        }

        public override string ToString()
        {
            return $"Scan {OrderNumber}: {SourceUrl}";
        }

        public bool IsFamilySearchLink =>
            !string.IsNullOrWhiteSpace(SourceUrl) &&
            SourceUrl.Contains("familysearch.org", StringComparison.OrdinalIgnoreCase);
    }
}
