using MuonRoiSocialNetwork.Common.Models.Users.Base.Response;

namespace MuonRoiSocialNetwork.Common.Models.Users.Response
{
    public class UserModelResponse : BaseUserResponse
    {
        public string? Name { get; set; }
        public bool IsDeleted { get; set; }
        public string? JwtToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
