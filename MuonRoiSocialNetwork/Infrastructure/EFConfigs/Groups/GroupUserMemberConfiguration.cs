using MuonRoi.Social_Network.Roles;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace MuonRoi.Social_Network.Configurations.Roles
{
    /// <summary>
    /// Configuration GroupUserMember
    /// </summary>
    public class GroupUserMemberConfiguration : IEntityTypeConfiguration<GroupUserMember>
    {
        /// <summary>
        /// Configuration GroupUserMember
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<GroupUserMember> builder)
        {
            builder.ToTable(nameof(GroupUserMember));
            builder.HasKey(x => new { x.AppUserKey, x.AppRoleKey });
            builder.HasOne(x => x.UserMember)
                .WithMany(x => x.GroupUserMember)
                .HasForeignKey(x => x.AppUserKey);

            builder.HasOne(x => x.AppRole)
                .WithMany(x => x.GroupUserMember)
                .HasForeignKey(x => x.AppRoleKey);
        }
    }
}
