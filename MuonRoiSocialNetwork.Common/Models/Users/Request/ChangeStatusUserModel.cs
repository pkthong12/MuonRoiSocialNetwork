using MuonRoi.Social_Network.Users;
using MuonRoiSocialNetwork.Common.Models.Users.Base.Request;

namespace MuonRoiSocialNetwork.Common.Models.Users.Request
{
    public class ChangeStatusUserModel : BaseUserRequest
    {
        public Guid Id { get; set; }
        public EnumAccountStatus AccountStatus { get; set; }
        public string? Reason { get; set; }
    }
}
