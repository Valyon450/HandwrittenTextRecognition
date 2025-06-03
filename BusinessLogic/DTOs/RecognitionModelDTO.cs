namespace BusinessLogic.DTOs
{
    public class RecognitionModelDTO
    {
        public Guid Id { get; set; }
        public byte[] ModelFile { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public FileInfo ModelInfo
        {
            get
            {
                return new FileInfo(FileName);
            }
        }
        public DateTime ImportTime { get; set; }
        public DateTime LastTrainedTime { get; set; }
    }
}
