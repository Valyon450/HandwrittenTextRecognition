using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Services.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BusinessLogic.Services
{
    public class RecognitionModelService : IRecognitionModelService
    {
        private readonly IHTRDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<RecognitionModelService> _logger;

        public RecognitionModelService(
            IHTRDbContext context,
            IMapper mapper,
            ILogger<RecognitionModelService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<RecognitionModelDTO>?> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                var models = await _context.RecognitionModel.ToListAsync(cancellationToken);
                return _mapper.Map<IEnumerable<RecognitionModelDTO>>(models);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching models.");
                return null;
            }
        }

        public async Task<RecognitionModelDTO?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var model = await _context.RecognitionModel.FindAsync(new object[] { id }, cancellationToken);

                if (model == null)
                {
                    throw new Exception($"Model with Id: {id} not found.");
                }

                return _mapper.Map<RecognitionModelDTO>(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while fetching model with Id: {id}.");
                throw;
            }
        }

        public async Task<Guid> CreateAsync(RecognitionModelDTO requestObject, CancellationToken cancellationToken)
        {
            try
            {
                var model = _mapper.Map<RecognitionModel>(requestObject);

                _context.RecognitionModel.Add(model);

                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation($"Model with Id: {model.Id} has been created successfully.");

                return model.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a model.");
                throw;
            }
        }

        public async Task UpdateAsync(RecognitionModelDTO requestObject, CancellationToken cancellationToken)
        {
            try
            {
                var model = await _context.RecognitionModel.FindAsync(new object[] { requestObject.Id }, cancellationToken);

                if (model == null)
                {
                    throw new Exception($"Model with Id: {requestObject.Id} not found.");
                }

                _mapper.Map(requestObject, model);

                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation($"Model with Id: {model.Id} has been updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating model with Id: {requestObject.Id}.");
                throw;
            }
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var model = await _context.RecognitionModel.FindAsync(new object[] { id }, cancellationToken);

                if (model == null)
                {
                    throw new Exception($"Model with Id: {id} not found.");
                }

                _context.RecognitionModel.Remove(model);
                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation($"Model with Id: {id} has been deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting model with Id: {id}.");
                throw;
            }
        }

    }
}
