using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Services.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BusinessLogic.Services
{
    public class InFileService : IInFileService
    {
        private readonly IHTRDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<InFileService> _logger;

        public InFileService(
            IHTRDbContext context,
            IMapper mapper,
            ILogger<InFileService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<InFileDTO>?> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                var files = await _context.InFile.ToListAsync(cancellationToken);
                return _mapper.Map<IEnumerable<InFileDTO>>(files);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching files.");
                return null;
            }
        }

        public async Task<InFileDTO?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var file = await _context.InFile.FindAsync(new object[] { id }, cancellationToken);

                if (file == null)
                {
                    throw new Exception($"File with Id: {id} not found.");
                }

                return _mapper.Map<InFileDTO>(file);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while fetching file with Id: {id}.");
                throw;
            }
        }

        public async Task<Guid> CreateAsync(InFileDTO requestObject, CancellationToken cancellationToken)
        {
            try
            {
                var file = _mapper.Map<InFile>(requestObject);

                _context.InFile.Add(file);

                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation($"File with Id: {file.Id} has been created successfully.");

                return file.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a file.");
                throw;
            }
        }

        public async Task UpdateAsync(InFileDTO requestObject, CancellationToken cancellationToken)
        {
            try
            {
                var file = await _context.InFile.FindAsync(new object[] { requestObject.Id }, cancellationToken);

                if (file == null)
                {
                    throw new Exception($"FIle with Id: {requestObject.Id} not found.");
                }

                _mapper.Map(requestObject, file);

                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation($"FIle with Id: {file.Id} has been updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating file with Id: {requestObject.Id}.");
                throw;
            }
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var file = await _context.InFile.FindAsync(new object[] { id }, cancellationToken);

                if (file == null)
                {
                    throw new Exception($"File with Id: {id} not found.");
                }

                _context.InFile.Remove(file);
                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation($"File with Id: {id} has been deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting file with Id: {id}.");
                throw;
            }
        }
    }
}
