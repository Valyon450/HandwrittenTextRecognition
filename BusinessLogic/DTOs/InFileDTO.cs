namespace BusinessLogic.DTOs
{
    public class InFileDTO
    {
        public Guid Id { get; set; }
        public byte[] Image { get; set; }
        public string FileName { get; set; }
        public FileInfo ScanInfo
        {
            get
            {
                return new FileInfo(FileName);
            }
        }
        public DateTime ImportTime { get; set; }
        public Guid USRSId { get; set; }
        public Guid TuningId { get; set; }
    }
}
