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
        /// Group id
        /// </summary>
        public override int Id { get; set; }
        /// <summary>
        /// Name of groups
        /// </summary>
        public string GroupName { get; set; } = string.Empty;
        /// <summary>
        /// UserMember
        /// </summary>
        public ICollection<AppUser>? UserMember { get; set; }
        /// <summary>
        /// AppRole
        /// </summary>
        public ICollection<AppRole>? AppRole { get; set; }
    }
}
