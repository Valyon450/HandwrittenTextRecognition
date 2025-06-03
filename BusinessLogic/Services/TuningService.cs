using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Services.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BusinessLogic.Services
{
    public class TuningService : ITuningService
    {
        private readonly IHTRDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<TuningService> _logger;

        public TuningService(
            IHTRDbContext context,
            IMapper mapper,
            ILogger<TuningService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<TuningDTO>?> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                var tunings = await _context.Tuning.ToListAsync(cancellationToken);
                return _mapper.Map<IEnumerable<TuningDTO>>(tunings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching tunings.");
                return null;
            }
        }

        public async Task<TuningDTO?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var tuning = await _context.Tuning.FindAsync(new object[] { id }, cancellationToken);

                if (tuning == null)
                {
                    throw new Exception($"Tuning with Id: {id} not found.");
                }

                return _mapper.Map<TuningDTO>(tuning);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while fetching tuning with Id: {id}.");
                throw;
            }
        }

        public async Task<Guid> CreateAsync(TuningDTO requestObject, CancellationToken cancellationToken)
        {
            try
            {
                var tuning = _mapper.Map<Tuning>(requestObject);

                _context.Tuning.Add(tuning);

                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation($"Tuning with Id: {tuning.Id} has been created successfully.");

                return tuning.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a tuning.");
                throw;
            }
        }

        public async Task UpdateAsync(TuningDTO requestObject, CancellationToken cancellationToken)
        {
            try
            {
                var tuning = await _context.Tuning.FindAsync(new object[] { requestObject.Id }, cancellationToken);

                if (tuning == null)
                {
                    throw new Exception($"Tuning with Id: {requestObject.Id} not found.");
                }

                _mapper.Map(requestObject, tuning);

                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation($"Tuning with Id: {tuning.Id} has been updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating tuning with Id: {requestObject.Id}.");
                throw;
            }
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var tuning = await _context.Tuning.FindAsync(new object[] { id }, cancellationToken);

                if (tuning == null)
                {
                    throw new Exception($"Tuning with Id: {id} not found.");
                }

                _context.Tuning.Remove(tuning);
                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation($"Tuning with Id: {id} has been deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting tuning with Id: {id}.");
                throw;
            }
        }

        /// <summary>
        /// Ініціалізує процес тренування для нової мови або шрифту.
        /// </summary>
        /// <param name="languageCode">Код мови, наприклад "ukr".</param>
        /// <param name="fontName">Назва шрифту для тренування.</param>
        /// <returns>Task з результатом ініціалізації.</returns>
        public async Task<bool> InitializeTrainingAsync(string languageCode, string fontName)
        {
            return true;
        }

        /// <summary>
        /// Додає новий тренувальний приклад (зображення + ground truth).
        /// </summary>
        /// <param name="imagePath">Шлях до зображення (TIFF/PNG).</param>
        /// <param name="groundTruthText">Очікуваний текст для цього зображення.</param>
        /// <returns>Task з результатом додавання прикладу.</returns>
        public async Task<bool> AddTrainingExampleAsync(string imagePath, string groundTruthText)
        {
            return true;
        }

        /// <summary>
        /// Запускає процес навчання моделі.
        /// </summary>
        /// <param name="outputDirectory">Каталог, де буде збережена модель.</param>
        /// <returns>Task з успішністю виконання.</returns>
        public async Task<bool> TrainModelAsync(string outputDirectory)
        {
            return true;
        }

        /// <summary>
        /// Завантажує натреновану модель у систему (копіює у tessdata).
        /// </summary>
        /// <param name="trainedDataPath">Шлях до .traineddata файлу.</param>
        /// <returns>Task з результатом завантаження.</returns>
        public async Task<bool> LoadTrainedModelAsync(string trainedDataPath)
        {
            return true;
        }

        /// <summary>
        /// Видаляє тимчасові або старі тренувальні дані.
        /// </summary>
        /// <returns>Task з результатом очищення.</returns>
        public async Task<bool> CleanupTrainingArtifactsAsync()
        {
            return true;
        }
    }
}
