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
        public string? GroupName { get; set; }
        /// <summary>
        /// AppUserKey
        /// </summary>
        public Guid AppUserKey { get; set; }
        /// <summary>
        /// AppRoleKey
        /// </summary>
        public Guid AppRoleKey { get; set; }
        /// <summary>
        /// UserMember
        /// </summary>
        public AppUser? UserMember { get; set; }
        /// <summary>
        /// AppRole
        /// </summary>
        public AppRole? AppRole { get; set; }
    }
}
