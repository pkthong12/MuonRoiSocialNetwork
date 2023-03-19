using MuonRoi.Social_Network.User;
namespace MuonRoiSocialNetwork.Common.Models.Users.Base.Request
{
    public class BaseUserRequest
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? UserName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public DateTime BirthDate { get; set; }
        public EnumGender Gender { get; set; }
    }
}
