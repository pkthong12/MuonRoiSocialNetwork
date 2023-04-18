using MuonRoi.Social_Network.Storys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MuonRoi.Social_Network.Configurations.Storys
{
    /// <summary>
    /// Configuration Story
    /// </summary>
    public class StoryConfiguration : IEntityTypeConfiguration<Story>
    {
        /// <summary>
        /// Configuration Story
        /// </summary>
        public void Configure(EntityTypeBuilder<Story> builder)
        {
            builder.ToTable((nameof(Story)));
            builder.HasKey(x => x.Guid);
            builder.Property(x => x.IsShow).HasDefaultValue(false);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.HasOne(x => x.Category)
                .WithMany(x => x.Storys)
                .HasForeignKey(x => x.CategoryId);
        }
    }
}
