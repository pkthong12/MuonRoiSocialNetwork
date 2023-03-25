using MuonRoi.Social_Network.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace MuonRoiSocialNetwork.Infrastructure.EFConfigs.Users
{
    /// <summary>
    /// Configuration AppUser
    /// </summary>
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        /// <summary>
        /// Configuration AppUser
        /// </summary>
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable(nameof(AppUser));
        }
    }
}
