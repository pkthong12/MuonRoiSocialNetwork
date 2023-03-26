using BaseConfig.EntityObject.Entity;
using BaseConfig.EntityObject.EntityObject;
using MuonRoi.Social_Network.Roles;
using MuonRoi.Social_Network.Storys;
using MuonRoi.Social_Network.User;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.Json.Serialization;
using MuonRoiSocialNetwork.Domains.DomainObjects.Users;

namespace MuonRoi.Social_Network.Users
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
        /// Tên đăng nhập
        /// </summary>
        /// <value></value>
        [Required(ErrorMessage = nameof(EnumUserErrorCodes.USR05C))]
        [MaxLength(100, ErrorMessage = nameof(EnumUserErrorCodes.USR10C))]
        [MinLength(5, ErrorMessage = nameof(EnumUserErrorCodes.USR15C))]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9_\.]{3,99}[a-z0-9](\@([a-zA-Z0-9][a-zA-Z0-9\.]+[a-zA-Z0-9]{2,}){1,5})?$", ErrorMessage = nameof(EnumUserErrorCodes.USR14C))]
        public override string UserName { get; set; }
        /// <summary>
        /// Mật khẩu
        /// </summary>
        /// <value></value>
        [Required(ErrorMessage = nameof(EnumUserErrorCodes.USR06C))]
        [MaxLength(1000, ErrorMessage = nameof(EnumUserErrorCodes.USR11C))]
        [MinLength(8, ErrorMessage = nameof(EnumUserErrorCodes.USR26C))]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = nameof(EnumUserErrorCodes.USR17C))]
        public override string? PasswordHash { get; set; }
        /// <summary>
        /// Email address
        /// </summary>
        /// <value></value>
        [MaxLength(1000, ErrorMessage = nameof(EnumUserErrorCodes.USR20C))]
        [RegularExpression(@"^(([^<>()[\]\\.,;:\s@\""]+(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$", ErrorMessage = nameof(EnumUserErrorCodes.USR19C))]
        public override string? Email { get; set; }
        /// <summary>
        /// Address''s User
        /// </summary>
        [MaxLength(1000, ErrorMessage = nameof(EnumUserErrorCodes.USR18C))]
        public string? Address { get; set; }
        /// <summary>
        /// BirthDate''s User
        /// </summary>
        [Required(ErrorMessage = nameof(EnumUserErrorCodes.USRC38C))]
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// Key hash password
        /// </summary>
        [MaxLength(1000, ErrorMessage = nameof(EnumUserErrorCodes.USR12C))]
        public string? Salt { get; set; }
        /// <summary>
        /// Gender''s User
        /// </summary>
        public EnumGender Gender { get; set; }
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
        /// <summary>
        /// Account status isOn | isOf
        /// </summary>
        public EnumAccountStatus AccountStatus { get; set; }
        /// <summary>
        /// Is renew pass
        /// </summary>
        public bool IsRenewPass { get; set; }
        /// <summary>
        /// CreatedDateTS
        /// </summary>
        public double? CreatedDateTS { get; set; }
        /// <summary>
        /// UpdatedDateTS
        /// </summary>

        public double? UpdatedDateTS { get; set; }
        /// <summary>
        /// DeletedDateTS
        /// </summary>

        public double? DeletedDateTS { get; set; }
        /// <summary>
        /// IsDeleted
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// Foreign key
        /// </summary>
        public GroupUserMember? GroupUserMember { get; set; }
        /// <summary>
        /// Foreign key
        /// </summary>
        public List<BookMarkStory>? BookMarkStory { get; set; }
        /// <summary>
        /// Foreign key
        /// </summary>
        public List<StoryNotifications>? StoryNotifications { get; set; }
        /// <summary>
        /// Foreign key
        /// </summary>
        public List<StoryPublish>? StoryPublish { get; set; }
        /// <summary>
        /// Foreign key
        /// </summary>
        public List<StoryReview>? StoryReview { get; set; }
        /// <summary>
        /// Foreign key
        /// </summary>
        public List<FollowingAuthor>? FollowingAuthor { get; set; }

        /// <summary>
        /// Foreign key
        /// </summary>
        public UserLogin? UserLoggin { get; set; }

        /// <summary>
        /// Foreign key
        /// </summary>
        protected List<ErrorResult> _errorMessages = new();

        /// <summary>
        /// ErrorMessages
        /// </summary>
        [JsonIgnore]
        public IReadOnlyCollection<ErrorResult> ErrorMessages => _errorMessages;
        /// <summary>
        /// GetAssembly
        /// </summary>
        /// <returns></returns>
        public Assembly? GetAssembly()
        {
            return GetType().Assembly;
        }
        /// <summary>
        /// IsValid
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            ValidationContext validationContext = new(this, null, null);
            List<ValidationResult> list = new();
            if (!Validator.TryValidateObject(this, validationContext, list, validateAllProperties: true))
            {
                foreach (ValidationResult item in list)
                {
                    ErrorResult errorResult = new ErrorResult
                    {
                        ErrorCode = item.ErrorMessage
                    };
                    errorResult.ErrorMessage = Helpers.GetErrorMessage(item.ErrorMessage, GetAssembly());
                    foreach (string memberName in item.MemberNames)
                    {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                        PropertyInfo property = validationContext.ObjectType.GetProperty(memberName);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                        object value = property.GetValue(validationContext.ObjectInstance, null);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                        errorResult.ErrorValues.Add(Helpers.GenerateErrorResult(memberName, value));
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                    }

                    _errorMessages.Add(errorResult);
                }
            }

            return _errorMessages.Count == 0;
        }
    }
}