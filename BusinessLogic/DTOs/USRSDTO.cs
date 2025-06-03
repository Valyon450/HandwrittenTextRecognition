namespace BusinessLogic.DTOs
{
    /// <summary>
    /// User Scan Recognition Session Data transfer object
    /// </summary>
    public class USRSDTO
    {
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public USRSStatusEnum Status { get; set; }
        public Guid? UserId { get; set; }
        public Guid? RecognitionResultId { get; set; }
        public Guid? RecognitionModelId { get; set; }
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
