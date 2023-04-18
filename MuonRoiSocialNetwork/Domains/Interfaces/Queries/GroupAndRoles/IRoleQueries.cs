using MuonRoiSocialNetwork.Common.Models.GroupAndRoles.Base.Response;

namespace MuonRoiSocialNetwork.Domains.Interfaces.Queries.GroupAndRoles
{
    /// <summary>
    /// Interfacec initial role queries
    /// </summary>
    public interface IRoleQueries
    {
        /// <summary>
        /// Get role by guid
        /// </summary>
        /// <param name="roleGuid"></param>
        /// <returns></returns>
        Task<RoleInitialBaseResponse> GetRoleByGuidAsync(Guid roleGuid);
        /// <summary>
        /// Get role by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<bool> GetRoleByNameAsync(string name);
    }
}
