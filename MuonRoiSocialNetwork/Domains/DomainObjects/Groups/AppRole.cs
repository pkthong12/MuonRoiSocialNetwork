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
        /// GroupUserMember
        /// </summary>
        public List<GroupUserMember>? GroupUserMember { get; set; }
    }
}
