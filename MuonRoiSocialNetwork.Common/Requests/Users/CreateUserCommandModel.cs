using ConnectVN.Social_Network.User;
using ConnectVN.Social_Network.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace MuonRoiSocialNetwork.Common.Requests.Users
{
    public class CreateUserCommandModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public EnumGender Gender { get; set; }
        public DateTime LastLogin { get; set; }
        public string Avatar { get; set; }
        public EnumAccountStatus Status { get; set; }
        public string Note { get; set; }
        public string Salt { get; set; }

    }
}
