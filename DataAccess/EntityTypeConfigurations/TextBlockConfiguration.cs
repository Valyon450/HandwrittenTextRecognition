using DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EntityTypeConfigurations
{
    public class TextBlockConfiguration : IEntityTypeConfiguration<TextBlock>
    {
        public void Configure(EntityTypeBuilder<TextBlock> builder)
        {
            builder.ToTable("TextBlock");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Text).IsRequired();
            builder.HasOne(x => x.Tuning).WithMany(t => t.TextBlocks).HasForeignKey(x => x.TuningId)
                .OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(x => x.InFile).WithMany(i => i.TextBlocks).HasForeignKey(x => x.InFileId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
