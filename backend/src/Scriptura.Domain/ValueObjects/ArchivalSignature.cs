namespace Scriptura.Domain.ValueObjects
{
    public record ArchivalSignature
    {
        public string ArchiveCode { get; init; }
        public string Fond { get; init; }
        public string Inventory { get; init; }
        public string ItemNumber { get; init; }

        public ArchivalSignature(string archiveCode, string fond, string inventory, string itemNumber)
        {
            if(string.IsNullOrWhiteSpace(archiveCode))
                throw new ArgumentException("Archive code cannot be empty.", nameof(archiveCode));

            if (string.IsNullOrWhiteSpace(fond))
                throw new ArgumentException("Fond number cannot be empty.", nameof(fond));

            if (string.IsNullOrWhiteSpace(inventory))
                throw new ArgumentException("Inventory number cannot be empty.", nameof(inventory));

            if (string.IsNullOrWhiteSpace(itemNumber))
                throw new ArgumentException("Item number cannot be empty.", nameof(itemNumber));

            ArchiveCode = archiveCode;
            Fond = fond;
            Inventory = inventory;
            ItemNumber = itemNumber;
        }

        public override string ToString()
        {
            return $"{ArchiveCode} {Fond}-{Inventory}-{ItemNumber}";
        }

        public bool IsEmpty =>
            string.IsNullOrWhiteSpace(ArchiveCode) &&
            string.IsNullOrWhiteSpace(Fond) &&
            string.IsNullOrWhiteSpace(Inventory) &&
            string.IsNullOrWhiteSpace(ItemNumber);

        public bool IsFullySpecified =>
            !string.IsNullOrWhiteSpace(ArchiveCode) &&
            !string.IsNullOrWhiteSpace(Fond) &&
            !string.IsNullOrWhiteSpace(Inventory) &&
            !string.IsNullOrWhiteSpace(ItemNumber);
    }
}
