using MuonRoi.Social_Network.Storys;
using MuonRoi.Social_Network.User;
using MuonRoi.Social_Network.Users;
using System.ComponentModel.DataAnnotations;

namespace MuonRoiSocialNetwork.Common.Models.Users
{
    public class UserModelRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public EnumGender Gender { get; set; }
        public DateTime LastLogin { get; set; }
        public string Avatar { get; set; }
        public EnumAccountStatus Status { get; set; }
        public string Note { get; set; }
        public string LockReason { get; set; }
        public int GroupId { get; set; }
        public string JwtToken { get; set; }
    }
}
