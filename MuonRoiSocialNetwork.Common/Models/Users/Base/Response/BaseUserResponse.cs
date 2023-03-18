using MuonRoi.Social_Network.Users;

namespace MuonRoiSocialNetwork.Common.Models.Users.Base.Response
{
    public class BaseUserResponse
    {
        public Guid Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public DateTime LastLogin { get; set; }
        public string? Avatar { get; set; }
        public EnumAccountStatus Status { get; set; }
        public string? Note { get; set; }
        public string? LockReason { get; set; }
        public int GroupId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string? RoleName { get; set; }
        public string? GroupName { get; set; }
    }
}
