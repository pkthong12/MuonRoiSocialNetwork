using ConnectVN.Social_Network.Roles;
using ConnectVN.Social_Network.Storys;
using ConnectVN.Social_Network.User;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
namespace ConnectVN.Social_Network.Users
{
    /// <summary>
    /// Table User Members
    /// </summary>
    public class AppUser : IdentityUser<Guid>
    {
        /// <summary>
        /// FirstName''s User
        /// </summary>
        [Required(ErrorMessage = nameof(EnumUserErrorCodes.USR03C))]
        [MaxLength(100, ErrorMessage = nameof(EnumUserErrorCodes.USR08C))]
        public string? Name { get; set; }
        /// <summary>
        /// LastName''s User
        /// </summary>
        [Required(ErrorMessage = nameof(EnumUserErrorCodes.USR04C))]
        [MaxLength(100, ErrorMessage = nameof(EnumUserErrorCodes.USR09C))]
        public string? Surname { get; set; }
        /// <summary>
        /// Address''s User
        /// </summary>
        [MaxLength(1000, ErrorMessage = nameof(EnumUserErrorCodes.USR18C))]
        public string? Address { get; set; }
        /// <summary>
        /// BirthDate''s User
        /// </summary>
        public DateTime? BirthDate { get; set; }
        /// <summary>
        /// Gender''s User
        /// </summary>
        public EnumGender? Gender { get; set; }
        /// <summary>
        /// Last login date
        /// </summary>
        /// <value></value>
        public DateTime? LastLogin { get; set; }
        /// <summary>
        /// Avatar Link
        /// </summary>
        /// <value></value>
        [MaxLength(1000, ErrorMessage = nameof(EnumNotificationStoryErrorCodes.NT01))]
        public string? Avatar { get; set; }

        /// <summary>
        /// Status of account
        /// </summary>
        /// <value></value>
        [Required(ErrorMessage = nameof(EnumUserErrorCodes.USR22C))]
        public EnumAccountStatus Status { get; set; }

        /// <summary>
        /// Ghi chú cho tài khoản
        /// </summary>
        /// <value></value>
        public string? Note { get; set; }

        /// <summary>
        /// Lý do khóa tài khoản
        /// </summary>
        /// <value></value>
        public string? LockReason { get; set; }

        /// <summary>
        /// GroupId of account
        /// </summary>
        public int? GroupId { get; set; }
        public List<GroupUserMember> GroupUserMember { get; set; }
        public List<BookMarkStory> BookMarkStory { get; set; }
        public List<StoryNotifications> StoryNotifications { get; set; }
        public List<StoryPublish> StoryPublish { get; set; }
        public List<StoryReview> StoryReview { get; set; }
        public List<FollowingAuthor> FollowingAuthor { get; set; }
    }
}