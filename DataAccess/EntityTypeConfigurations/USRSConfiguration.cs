using DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EntityTypeConfigurations
{
    internal class USRSConfiguration : IEntityTypeConfiguration<USRS>
    {
        public void Configure(EntityTypeBuilder<USRS> builder)
        {
            builder.ToTable("USRS");
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.User).WithMany(u => u.USRSs).HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(x => x.RecognitionModel).WithMany(r => r.USRSs).HasForeignKey(x => x.RecognitionModelId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
