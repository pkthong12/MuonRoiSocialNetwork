using MuonRoi.Social_Network.Roles;
using Microsoft.AspNetCore.Identity;

namespace MuonRoiSocialNetwork.Domains.DomainObjects.Groups
{
    /// <summary>
    /// AppRole
    /// </summary>
    public class AppRole : IdentityRole<Guid>
    {
        /// <summary>
        /// Description
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// IsDeleted
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// Group id
        /// </summary>
        public int GroupId { get; set; }
        /// <summary>
        /// GroupUserMember
        /// </summary>
        public GroupUserMember? GroupUserMember { get; set; }
    }
}
