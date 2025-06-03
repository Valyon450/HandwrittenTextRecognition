namespace DataAccess.Entities
{
    /// <summary>
    /// Fine-tuning Session of Recognition Model
    /// </summary>
    public class Tuning
    {
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public TuningStatusEnum Status { get; set; }
        public Guid? UserId { get; set; }
        public User? User { get; set; }
        public Guid? RecognitionModelId { get; set; }
        public RecognitionModel? RecognitionModel { get; set; }
        public ICollection<InFile>? InFiles { get; set; }
        public ICollection<TextBlock>? TextBlocks { get; set; }
    }

    public enum TuningStatusEnum
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
        /// Помилка
        /// </summary>
        Error,

        /// <summary>
        /// Скасовано
        /// </summary>
        Cancelled,
    }
}
