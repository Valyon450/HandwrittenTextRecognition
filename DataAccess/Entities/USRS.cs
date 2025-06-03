namespace DataAccess.Entities
{
    /// <summary>
    /// User Scan Recognition Session
    /// </summary>
    public class USRS
    {
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public USRSStatusEnum Status { get; set; }
        public Guid? UserId { get; set; }
        public User? User { get; set; }        
        public Guid? RecognitionResultId { get; set; }
        public RecognitionResult? RecognitionResult { get; set; }
        public Guid? RecognitionModelId  { get; set; }
        public RecognitionModel? RecognitionModel { get; set; }
        public ICollection<InFile>? InFiles { get; set; }
    }

    public enum USRSStatusEnum
    {
        /// <summary>
        /// В процесі
        /// </summary>
        InProgress,

        /// <summary>
        /// Завершено
        /// </summary>
        Completed,

        /// <summary>
        /// Скасовано
        /// </summary>
        Cancelled,
    }
}