using DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EntityTypeConfigurations
{
    public class RecognitionModelConfiguration : IEntityTypeConfiguration<RecognitionModel>
    {
        public void Configure(EntityTypeBuilder<RecognitionModel> builder)
        {
            builder.ToTable("RecognitionModel");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FileName).IsRequired().HasMaxLength(100);
            builder.Ignore(x => x.ModelInfo);
            builder.Property(x => x.Description).HasMaxLength(500);
        }
    }
}
