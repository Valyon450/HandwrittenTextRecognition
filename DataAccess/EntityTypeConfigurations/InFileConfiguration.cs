using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityTypeConfigurations
{
    public class InFileConfiguration : IEntityTypeConfiguration<InFile>
    {
        public void Configure(EntityTypeBuilder<InFile> builder)
        {
            builder.ToTable("InFile");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FileName).IsRequired().HasMaxLength(255);
            builder.Ignore(x => x.ScanInfo);
            builder.HasOne(x => x.USRS).WithMany(u => u.InFiles).HasForeignKey(x => x.USRSId)
                .OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(x => x.Tuning).WithMany(t => t.InFiles).HasForeignKey(x => x.TuningId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
