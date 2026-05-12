using Scriptura.Domain.Entities.Catalog;

namespace Scriptura.Domain.Repositories;

public interface ISettlementRepository
{
    Task<Settlement?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Settlement>> GetAllAsync(CancellationToken cancellationToken = default);

    void Add(Settlement settlement);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Settlement>> SearchByNameAsync(string query, CancellationToken cancellationToken = default);
}