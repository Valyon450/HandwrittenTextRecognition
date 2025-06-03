using DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EntityTypeConfigurations
{
    internal class TuningConfiguration : IEntityTypeConfiguration<Tuning>
    {
        public void Configure(EntityTypeBuilder<Tuning> builder)
        {
            builder.ToTable("Tuning");
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.User).WithMany(u => u.Tunings).HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(x => x.RecognitionModel).WithMany(r => r.Tunings).HasForeignKey(x => x.RecognitionModelId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
