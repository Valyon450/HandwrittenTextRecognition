using Microsoft.EntityFrameworkCore;
using Scriptura.Domain.Entities.Catalog;
using Scriptura.Domain.Repositories;

namespace Scriptura.Infrastructure.Postgres.Repositories;

internal sealed class ArchivalItemRepository(ScripturaDbContext dbContext)
    : PostgresRepositoryBase(dbContext), IArchivalItemRepository
{
    public async Task<ArchivalItem?> GetByIdWithScansAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<ArchivalItem>()
            .Include(x => x.Scans)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ArchivalItem>> GetAllAsync(Guid? settlementId = null, CancellationToken cancellationToken = default)
    {
        var query = DbContext.Set<ArchivalItem>().AsNoTracking();

        if (settlementId.HasValue)
        {
            query = query.Where(x => x.SettlementIds.Contains(settlementId.Value));
        }

        return await query.ToListAsync(cancellationToken);
    }

    public void Add(ArchivalItem item)
    {
        DbContext.Set<ArchivalItem>().Add(item);
    }

    public void Remove(ArchivalItem item)
    {
        DbContext.Set<ArchivalItem>().Remove(item);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await DbContext.SaveChangesAsync(cancellationToken);
    }
}