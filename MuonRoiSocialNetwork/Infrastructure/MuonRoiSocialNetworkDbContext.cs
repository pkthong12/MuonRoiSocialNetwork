using MuonRoi.Social_Network.Categories;
using MuonRoi.Social_Network.Chapters;
using MuonRoi.Social_Network.Configurations.Categories;
using MuonRoi.Social_Network.Configurations.Chapters;
using MuonRoi.Social_Network.Configurations.Roles;
using MuonRoi.Social_Network.Configurations.Storys;
using MuonRoi.Social_Network.Configurations.Tags;
using MuonRoi.Social_Network.Configurations.Users;
using MuonRoi.Social_Network.Roles;
using MuonRoi.Social_Network.Storys;
using MuonRoi.Social_Network.Tags;
using MuonRoi.Social_Network.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MuonRoiSocialNetwork.Domains.DomainObjects.Groups;
using MuonRoiSocialNetwork.Infrastructure.EFConfigs.Groups;
using MuonRoiSocialNetwork.Infrastructure.Extentions;
using BaseConfig.BaseDbContext;
using MediatR;

namespace MuonRoiSocialNetwork.Infrastructure
{
    /// <summary>
    /// Dbcontext config
    /// </summary>
    public class MuonRoiSocialNetworkDbContext : BaseDbContext
    {
        /// <summary>
        /// Constructor dbcontext
        /// </summary>
        /// <param name="options"></param>
        /// <param name="mediator"></param>
#pragma warning disable CS8618
        public MuonRoiSocialNetworkDbContext(DbContextOptions<MuonRoiSocialNetworkDbContext> options, IMediator mediator) : base(options, mediator)
#pragma warning restore CS8618
        { }
        /// <summary>
        /// Apply config
        /// </summary>
        /// <param name="builder"></param>
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
        /// <summary>
        /// 
        /// </summary>
        public DbSet<Category> Categories { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<Chapter> Chapters { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<GroupUserMember> GroupUserMembers { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<Story> Stories { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<StoryNotifications> StoryNotifications { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<StoryPublish> StoryPublishes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<StoryReview> StoryReviews { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<Tag> Tags { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<TagInStory> TagInStories { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<AppUser> AppUsers { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<BookMarkStory> BookMarkStories { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<FollowingAuthor> FollowingAuthors { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<AppRole> AppRoles { get; set; }
    }
}
