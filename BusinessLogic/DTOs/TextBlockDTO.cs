namespace BusinessLogic.DTOs
{
    public class TextBlockDTO
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public TextTypeEnum TextType { get; set; }

        /// <summary>
        /// Вказує на те, чи є даний текст еталонним.
        /// </summary>
        ///
        public bool IsBenchmark { get; set; }

        /// <summary>
        /// Координата по X розташування текстового блоку (верхній лівий кут) 
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Координата по Y розташування текстового блоку (верхній лівий кут)
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Ширина блоку (у px)
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Висота блоку (у px)
        /// </summary>
        public int Height { get; set; }

        public Guid? TuningId { get; set; }

        public Guid? InFileId { get; set; }
    }

    public enum TextTypeEnum
    {
        /// <summary>
        /// Заголовок
        /// </summary>
        Header,

        /// <summary>
        /// Абзац
        /// </summary>
        Paragraph,

        /// <summary>
        /// Примітка
        /// </summary>
        Footnote,

        /// <summary>
        /// Таблиця
        /// </summary>
        Table,

        /// <summary>
        /// Інший тип
        /// </summary>
        Other
    }
}
