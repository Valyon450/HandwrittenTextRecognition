namespace DataAccess.Entities
{
    public class RecognitionModel
    {
        public Guid Id { get; set; }
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
        public ICollection<USRS>? USRSs { get; set; }
        public ICollection<Tuning>? Tunings { get; set; }
    }
}
