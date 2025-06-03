namespace BusinessLogic.Services.Interfaces
{
    public interface ICRUD<DTO> where DTO : class
    {
        Task<IEnumerable<DTO>?> GetAllAsync(CancellationToken cancellationToken);

        Task<DTO?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<Guid> CreateAsync(DTO model, CancellationToken cancellationToken);

        Task UpdateAsync(DTO model, CancellationToken cancellationToken);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
