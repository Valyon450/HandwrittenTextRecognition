using BusinessLogic.DTOs;

namespace BusinessLogic.Services.Interfaces
{
    public interface IUSRSService : ICRUD<USRSDTO>
    {
        /// <summary>
        /// Розпізнає текст із зображення.
        /// </summary>
        /// <param name="imagePath">Шлях до зображення.</param>
        /// <param name="language">Код мови для Tesseract (наприклад, "ukr", "eng").</param>
        /// <returns>Розпізнаний текст.</returns>
        Task<string> RecognizeTextAsync(string imagePath, string language = "ukr");

        /// <summary>
        /// Розпізнає текст із переданого масиву байтів зображення.
        /// </summary>
        /// <param name="imageBytes">Байтовий масив зображення.</param>
        /// <param name="language">Код мови (наприклад, "ukr", "eng").</param>
        /// <returns>Розпізнаний текст.</returns>
        // Task<string> RecognizeTextFromBytesAsync(byte[] imageBytes, string language = "ukr");

        /// <summary>
        /// Отримує додаткову інформацію з розпізнаного зображення: впевненість, кількість символів, тощо.
        /// </summary>
        /// <param name="imagePath">Шлях до зображення.</param>
        /// <param name="language">Код мови.</param>
        /// <returns>Обʼєкт з деталями розпізнавання.</returns>
        Task<RecognitionResultDTO> GetDetailedRecognitionAsync(string imagePath, string language = "ukr");
    }
}
