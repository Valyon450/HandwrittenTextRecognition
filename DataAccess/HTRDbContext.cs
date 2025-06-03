using DataAccess.Entities;
using DataAccess.EntityTypeConfigurations;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class HTRDbContext : DbContext, IHTRDbContext
    {
        public DbSet<InFile> InFile { get; set; }
        public DbSet<RecognitionModel> RecognitionModel { get; set; }
        public DbSet<RecognitionResult> RecognitionResult { get; set; }
        public DbSet<TextBlock> TextBlock { get; set; }
        public DbSet<Tuning> Tuning { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<USRS> USRS { get; set; }

        public HTRDbContext() { }

        public HTRDbContext(DbContextOptions<HTRDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=localhost\\SQLExpress;Database=HandwrittenTextRecognitionDb;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new InFileConfiguration());
            modelBuilder.ApplyConfiguration(new RecognitionModelConfiguration());
            modelBuilder.ApplyConfiguration(new RecognitionResultConfiguration());
            modelBuilder.ApplyConfiguration(new TextBlockConfiguration());
            modelBuilder.ApplyConfiguration(new TuningConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new USRSConfiguration());            
        }
    }
}
