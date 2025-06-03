using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Services.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IHTRDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        public UserService(
            IHTRDbContext context,
            IMapper mapper,
            ILogger<UserService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<UserDTO>?> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                var users = await _context.User.ToListAsync(cancellationToken);
                return _mapper.Map<IEnumerable<UserDTO>>(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching users.");
                return null;
            }
        }

        public async Task<UserDTO?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _context.User.FindAsync(new object[] { id }, cancellationToken);

                if (user == null)
                {
                    throw new Exception($"User with Id: {id} not found.");
                }

                return _mapper.Map<UserDTO>(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while fetching user with Id: {id}.");
                throw;
            }
        }

        public async Task<Guid> CreateAsync(UserDTO requestObject, CancellationToken cancellationToken)
        {
            try
            {
                var user = _mapper.Map<User>(requestObject);

                _context.User.Add(user);

                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation($"User with Id: {user.Id} has been created successfully.");

                return user.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a user.");
                throw;
            }
        }

        public async Task UpdateAsync(UserDTO requestObject, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _context.User.FindAsync(new object[] { requestObject.Id }, cancellationToken);

                if (user == null)
                {
                    throw new Exception($"User with Id: {requestObject.Id} not found.");
                }

                _mapper.Map(requestObject, user);

                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation($"User with Id: {user.Id} has been updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating user with Id: {requestObject.Id}.");
                throw;
            }
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _context.User.FindAsync(new object[] { id }, cancellationToken);

                if (user == null)
                {
                    throw new Exception($"User with Id: {id} not found.");
                }

                _context.User.Remove(user);
                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation($"User with Id: {id} has been deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting user with Id: {id}.");
                throw;
            }
        }
    }
}
