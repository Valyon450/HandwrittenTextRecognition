using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DataAccess.Entities;

namespace DataAccess.EntityTypeConfigurations
{
    public class RecognitionResultConfiguration : IEntityTypeConfiguration<RecognitionResult>
    {
        public void Configure(EntityTypeBuilder<RecognitionResult> builder)
        {
            builder.ToTable("RecognitionResult");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.RecognizedText).IsRequired();
            builder.HasOne(x => x.USRS).WithOne(u => u.RecognitionResult).HasForeignKey<RecognitionResult>(x => x.USRSId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
