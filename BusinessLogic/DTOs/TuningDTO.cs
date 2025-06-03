namespace BusinessLogic.DTOs
{
    public class TuningDTO
    {
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public TuningStatusEnum Status { get; set; }
        public Guid? UserId { get; set; }
        public Guid? RecognitionModelId { get; set; }
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
