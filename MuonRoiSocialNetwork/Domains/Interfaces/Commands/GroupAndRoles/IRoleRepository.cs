using MuonRoiSocialNetwork.Common.Models.GroupAndRoles.Base.Request;
using MuonRoiSocialNetwork.Domains.DomainObjects.Groups;

namespace MuonRoiSocialNetwork.Domains.Interfaces.Commands.GroupAndRoles
{
    /// <summary>
    /// Interface init role methods
    /// </summary>
    public interface IRoleRepository
    {
        /// <summary>
        /// Init role
        /// </summary>
        /// <param name="approleRequest"></param>
        /// <returns></returns>
        Task<int> InitialRoleAsync(AppRole approleRequest);
        /// <summary>
        /// Update role info
        /// </summary>
        /// <param name="infoRole"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(AppRole infoRole);
    }
}
