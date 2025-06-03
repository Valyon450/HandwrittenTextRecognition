using System.Globalization;

namespace DataAccess.Entities
{
    public class RecognitionResult
    {
        public Guid Id { get; set; }

        public string RecognizedText { get; set; }

        /// <summary>
        /// Час, коли було здійснено розпізнавання.
        /// </summary>
        public DateTime RecognitionTime { get; set; }

        /// <summary>
        /// Середній рівень впевненості моделі (внутрішній показник)
        /// </summary>
        public float AverageConfidence { get; set; }

        /// <summary>
        /// Точність розпізнавання (зовнішній показник)
        /// </summary>
        public float Accuracy { get; set; }

        /// <summary>
        /// CER (Character Error Rate) - значення помилок на рівні символів
        /// </summary>
        public float CharacterErrorRate { get; set; }

        /// <summary>
        /// WER (Word Error Rate) - значення помилок на рівні слів
        /// </summary>
        public float WordErrorRate { get; set; }

        /// <summary>
        /// Тривалість процесу розпізнавання.
        /// </summary>
        public float ProcessingTime { get; set; }

        public Guid? USRSId { get; set; }
        public USRS? USRS { get; set; }
    }
}
