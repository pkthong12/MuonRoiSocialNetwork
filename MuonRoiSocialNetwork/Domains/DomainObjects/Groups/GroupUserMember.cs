using BaseConfig.EntityObject.Entity;
using MuonRoi.Social_Network.Users;
using MuonRoiSocialNetwork.Domains.DomainObjects.Groups;

namespace MuonRoi.Social_Network.Roles
{
    /// <summary>
    /// Table of group user
    /// </summary>
    public class GroupUserMember : Entity
    {
        /// <summary>
        /// Name of groups
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// Guest (haven't account)
        /// </summary>
        public Guid AppUserKey { get; set; }
        public Guid AppRoleKey { get; set; }

        public AppUser UserMember { get; set; }
        public AppRole AppRole { get; set; }
    }
}
