using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Interfaces
{
    public interface IHTRDbContext
    {
        DbSet<InFile> InFile { get; set; }
        DbSet<RecognitionModel> RecognitionModel { get; set; }
        DbSet<RecognitionResult> RecognitionResult { get; set; }
        DbSet<TextBlock> TextBlock { get; set; }
        DbSet<Tuning> Tuning { get; set; }
        DbSet<User> User { get; set; }
        DbSet<USRS> USRS { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
