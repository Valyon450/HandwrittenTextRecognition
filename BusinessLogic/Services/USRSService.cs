using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Services.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tesseract;

namespace BusinessLogic.Services
{
    public class USRSService : IUSRSService
    {
        private readonly IHTRDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<USRSService> _logger;

        public USRSService(
            IHTRDbContext context,
            IMapper mapper,
            ILogger<USRSService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<USRSDTO>?> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                var sessions = await _context.USRS.ToListAsync(cancellationToken);
                return _mapper.Map<IEnumerable<USRSDTO>>(sessions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching sessions.");
                return null;
            }
        }

        public async Task<USRSDTO?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var session = await _context.USRS.FindAsync(new object[] { id }, cancellationToken);

                if (session == null)
                {
                    throw new Exception($"Session with Id: {id} not found.");
                }

                return _mapper.Map<USRSDTO>(session);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while fetching session with Id: {id}.");
                throw;
            }
        }

        public async Task<Guid> CreateAsync(USRSDTO requestObject, CancellationToken cancellationToken)
        {
            try
            {
                var session = _mapper.Map<USRS>(requestObject);

                _context.USRS.Add(session);

                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation($"Session with Id: {session.Id} has been created successfully.");

                return session.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a session.");
                throw;
            }
        }

        public async Task UpdateAsync(USRSDTO requestObject, CancellationToken cancellationToken)
        {
            try
            {
                var session = await _context.USRS.FindAsync(new object[] { requestObject.Id }, cancellationToken);

                if (session == null)
                {
                    throw new Exception($"Session with Id: {requestObject.Id} not found.");
                }

                _mapper.Map(requestObject, session);

                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation($"Session with Id: {session.Id} has been updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating session with Id: {requestObject.Id}.");
                throw;
            }
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var session = await _context.USRS.FindAsync(new object[] { id }, cancellationToken);

                if (session == null)
                {
                    throw new Exception($"Session with Id: {id} not found.");
                }

                _context.USRS.Remove(session);
                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation($"Session with Id: {id} has been deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting session with Id: {id}.");
                throw;
            }
        }

        /// <summary>
        /// Розпізнає текст із зображення.
        /// </summary>
        /// <param name="imagePath">Шлях до зображення.</param>
        /// <param name="language">Код мови для Tesseract (наприклад, "ukr", "eng").</param>
        /// <returns>Розпізнаний текст.</returns>
        public async Task<string> RecognizeTextAsync(string imagePath, string language = "ukr")
        {
            try
            {
                string tessDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tessdata");

                using var engine = new TesseractEngine(tessDataPath, language, EngineMode.Default);
                using var img = Pix.LoadFromFile(imagePath);
                using var page = engine.Process(img);

                return page.GetText();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during text recognition.");
                return string.Empty;
            }
        }

        /// <summary>
        /// Отримує додаткову інформацію з розпізнаного зображення: впевненість, кількість символів, тощо.
        /// </summary>
        /// <param name="imagePath">Шлях до зображення.</param>
        /// <param name="language">Код мови.</param>
        /// <returns>Обʼєкт з деталями розпізнавання.</returns>
        public async Task<RecognitionResultDTO> GetDetailedRecognitionAsync(string imagePath, string language = "ukr")
        {
            return new RecognitionResultDTO();
        }
    }
}