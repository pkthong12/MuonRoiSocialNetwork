using ConnectVN.Social_Network.Categories;
using ConnectVN.Social_Network.Chapters;
using ConnectVN.Social_Network.Configurations.Categories;
using ConnectVN.Social_Network.Configurations.Chapters;
using ConnectVN.Social_Network.Configurations.Roles;
using ConnectVN.Social_Network.Configurations.Storys;
using ConnectVN.Social_Network.Configurations.Tags;
using ConnectVN.Social_Network.Configurations.Users;
using ConnectVN.Social_Network.Roles;
using ConnectVN.Social_Network.Storys;
using ConnectVN.Social_Network.Tags;
using ConnectVN.Social_Network.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MuonRoiSocialNetwork.Domains.DomainObjects.Groups;
using MuonRoiSocialNetwork.Infrastructure.EFConfigs.Groups;
using MuonRoiSocialNetwork.Infrastructure.Extentions;

namespace MuonRoiSocialNetwork.Infrastructure
{
    public class MuonRoiSocialNetworkDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public MuonRoiSocialNetworkDbContext(DbContextOptions options) : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new ChapterConfiguration());
            builder.ApplyConfiguration(new GroupUserMemberConfiguration());
            builder.ApplyConfiguration(new StoryConfiguration());
            builder.ApplyConfiguration(new StoryNotificationConfiguration());
            builder.ApplyConfiguration(new StoryPublishConfiguration());
            builder.ApplyConfiguration(new StoryReviewConfiguration());
            builder.ApplyConfiguration(new TagConfiguration());
            builder.ApplyConfiguration(new TagInStoryConfiguration());
            builder.ApplyConfiguration(new BookMarkStoryConfiguration());
            builder.ApplyConfiguration(new FollowingAuthorConfiguration());
            builder.ApplyConfiguration(new AppRoleConfiguration());

            builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaim");
            builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRole").HasKey(x => new { x.RoleId, x.UserId });
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogin").HasKey(x => x.UserId);
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaim");
            builder.Entity<IdentityUserToken<Guid>>().ToTable("UserToken").HasKey(x => x.UserId);
            builder.Seed();
            base.OnModelCreating(builder);

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<GroupUserMember> GroupUserMembers { get; set; }
        public DbSet<Story> Stories { get; set; }
        public DbSet<StoryNotifications> StoryNotifications { get; set; }
        public DbSet<StoryPublish> StoryPublishes { get; set; }
        public DbSet<StoryReview> StoryReviews { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagInStory> TagInStories { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<BookMarkStory> BookMarkStories { get; set; }
        public DbSet<FollowingAuthor> FollowingAuthors { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
    }
}
