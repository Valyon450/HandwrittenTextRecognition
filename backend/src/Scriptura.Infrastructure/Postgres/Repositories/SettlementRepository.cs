using Microsoft.EntityFrameworkCore;
using Scriptura.Domain.Entities.Catalog;
using Scriptura.Domain.Repositories;

namespace Scriptura.Infrastructure.Postgres.Repositories;

internal sealed class SettlementRepository(ScripturaDbContext dbContext)
    : PostgresRepositoryBase(dbContext), ISettlementRepository
{
    public Task<Settlement?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => DbContext.Set<Settlement>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<IReadOnlyList<Settlement>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Settlement>().AsNoTracking().ToListAsync(cancellationToken);
    }

    public void Add(Settlement settlement)
        => DbContext.Set<Settlement>().Add(settlement);

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        => DbContext.SaveChangesAsync(cancellationToken);

    public async Task<IReadOnlyList<Settlement>> SearchByNameAsync(string query, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(query))
            return [];

        var normalizedQuery = query.ToLower();

        return await DbContext.Set<Settlement>()
            .AsNoTracking()
            .Where(s => s.CurrentName.ToLower().Contains(normalizedQuery))
            .ToListAsync(cancellationToken);
    }

    public void Remove(Settlement settlement)
    {
        DbContext.Set<Settlement>().Remove(settlement);
    }
}