using BusinessLogic.DTOs;

namespace BusinessLogic.Services.Interfaces
{
    public interface ITuningService : ICRUD<TuningDTO>
    {
        /// <summary>
        /// Ініціалізує процес тренування для нової мови або шрифту.
        /// </summary>
        /// <param name="languageCode">Код мови, наприклад "ukr".</param>
        /// <param name="fontName">Назва шрифту для тренування.</param>
        /// <returns>Task з результатом ініціалізації.</returns>
        Task<bool> InitializeTrainingAsync(string languageCode, string fontName);

        /// <summary>
        /// Додає новий тренувальний приклад (зображення + ground truth).
        /// </summary>
        /// <param name="imagePath">Шлях до зображення (TIFF/PNG).</param>
        /// <param name="groundTruthText">Очікуваний текст для цього зображення.</param>
        /// <returns>Task з результатом додавання прикладу.</returns>
        Task<bool> AddTrainingExampleAsync(string imagePath, string groundTruthText);

        /// <summary>
        /// Запускає процес навчання моделі.
        /// </summary>
        /// <param name="outputDirectory">Каталог, де буде збережена модель.</param>
        /// <returns>Task з успішністю виконання.</returns>
        Task<bool> TrainModelAsync(string outputDirectory);

        /// <summary>
        /// Завантажує натреновану модель у систему (копіює у tessdata).
        /// </summary>
        /// <param name="trainedDataPath">Шлях до .traineddata файлу.</param>
        /// <returns>Task з результатом завантаження.</returns>
        Task<bool> LoadTrainedModelAsync(string trainedDataPath);

        /// <summary>
        /// Видаляє тимчасові або старі тренувальні дані.
        /// </summary>
        /// <returns>Task з результатом очищення.</returns>
        Task<bool> CleanupTrainingArtifactsAsync();
    }
}
