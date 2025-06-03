using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Services.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BusinessLogic.Services
{
    public class RecognitionResultService : IRecognitionResultService
    {
        private readonly IHTRDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<RecognitionResultService> _logger;

        public RecognitionResultService(
            IHTRDbContext context,
            IMapper mapper,
            ILogger<RecognitionResultService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<RecognitionResultDTO>?> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                var results = await _context.RecognitionResult.ToListAsync(cancellationToken);
                return _mapper.Map<IEnumerable<RecognitionResultDTO>>(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching results.");
                return null;
            }
        }

        public async Task<RecognitionResultDTO?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _context.RecognitionResult.FindAsync(new object[] { id }, cancellationToken);

                if (result == null)
                {
                    throw new Exception($"Result with Id: {id} not found.");
                }

                return _mapper.Map<RecognitionResultDTO>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while fetching result with Id: {id}.");
                throw;
            }
        }

        public async Task<Guid> CreateAsync(RecognitionResultDTO requestObject, CancellationToken cancellationToken)
        {
            try
            {
                var result = _mapper.Map<RecognitionResult>(requestObject);

                _context.RecognitionResult.Add(result);

                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation($"Result with Id: {result.Id} has been created successfully.");

                return result.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a result.");
                throw;
            }
        }

        public async Task UpdateAsync(RecognitionResultDTO requestObject, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _context.RecognitionResult.FindAsync(new object[] { requestObject.Id }, cancellationToken);

                if (result == null)
                {
                    throw new Exception($"Result with Id: {requestObject.Id} not found.");
                }

                _mapper.Map(requestObject, result);

                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation($"Result with Id: {result.Id} has been updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating result with Id: {requestObject.Id}.");
                throw;
            }
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _context.RecognitionResult.FindAsync(new object[] { id }, cancellationToken);

                if (result == null)
                {
                    throw new Exception($"Result with Id: {id} not found.");
                }

                _context.RecognitionResult.Remove(result);
                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation($"Result with Id: {id} has been deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting result with Id: {id}.");
                throw;
            }
        }
    }
}
