using Scriptura.Domain.Entities.Catalog;

namespace Scriptura.Domain.Repositories
{
    public interface IArchivalItemRepository
    {
        Task<ArchivalItem?> GetByIdWithScansAsync(Guid id, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<ArchivalItem>> GetAllAsync(Guid? settlementId = null, CancellationToken cancellationToken = default);

        void Add(ArchivalItem item);        

        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
