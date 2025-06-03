using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Services.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BusinessLogic.Services
{
    public class TextBlockService : ITextBlockService
    {
        private readonly IHTRDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<TextBlockService> _logger;

        public TextBlockService(
            IHTRDbContext context,
            IMapper mapper,
            ILogger<TextBlockService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<TextBlockDTO>?> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                var textBlocks = await _context.TextBlock.ToListAsync(cancellationToken);
                return _mapper.Map<IEnumerable<TextBlockDTO>>(textBlocks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching textBlock.");
                return null;
            }
        }

        public async Task<TextBlockDTO?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var textBlock = await _context.TextBlock.FindAsync(new object[] { id }, cancellationToken);

                if (textBlock == null)
                {
                    throw new Exception($"TextBlock with Id: {id} not found.");
                }

                return _mapper.Map<TextBlockDTO>(textBlock);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while fetching textBlock with Id: {id}.");
                throw;
            }
        }

        public async Task<Guid> CreateAsync(TextBlockDTO requestObject, CancellationToken cancellationToken)
        {
            try
            {
                var textBlock = _mapper.Map<TextBlock>(requestObject);

                _context.TextBlock.Add(textBlock);

                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation($"TextBlock with Id: {textBlock.Id} has been created successfully.");

                return textBlock.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a textBlock.");
                throw;
            }
        }

        public async Task UpdateAsync(TextBlockDTO requestObject, CancellationToken cancellationToken)
        {
            try
            {
                var textBlock = await _context.TextBlock.FindAsync(new object[] { requestObject.Id }, cancellationToken);

                if (textBlock == null)
                {
                    throw new Exception($"TextBlock with Id: {requestObject.Id} not found.");
                }

                _mapper.Map(requestObject, textBlock);

                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation($"TextBlock with Id: {textBlock.Id} has been updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating textBlock with Id: {requestObject.Id}.");
                throw;
            }
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var textBlock = await _context.TextBlock.FindAsync(new object[] { id }, cancellationToken);

                if (textBlock == null)
                {
                    throw new Exception($"TextBlock with Id: {id} not found.");
                }

                _context.TextBlock.Remove(textBlock);
                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation($"TextBlock with Id: {id} has been deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting textBlock with Id: {id}.");
                throw;
            }
        }
    }
}
