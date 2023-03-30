using MuonRoiSocialNetwork.Domains.DomainObjects.Groups;
using MuonRoiSocialNetwork.Domains.Interfaces.Commands.GroupAndRoles;

namespace MuonRoiSocialNetwork.Infrastructure.Repositories.GroupAndRoles
{
    /// <summary>
    /// Role Repository
    /// </summary>
    public class RoleRepository : IRoleRepository
    {
        private readonly MuonRoiSocialNetworkDbContext _dbcontext;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbcontext"></param>
        public RoleRepository(MuonRoiSocialNetworkDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        /// <summary>
        /// Handler init new role
        /// </summary>
        /// <param name="approleRequest"></param>
        /// <returns></returns>
        public async Task<int> InitialRoleAsync(AppRole approleRequest)
        {
            try
            {
                if (approleRequest is null)
                {
                    return -1;
                }
                _dbcontext?.AppRoles?.Add(approleRequest);

#pragma warning disable CS8602
                return await _dbcontext?.SaveChangesAsync();
#pragma warning restore CS8602

            }
            catch
            {
                return -1;
            }
        }
        /// <summary>
        /// Handler update role
        /// </summary>
        /// <param name="infoRole"></param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(AppRole infoRole)
        {
            try
            {
                if (infoRole is null)
                {
                    return -1;
                }
                _dbcontext?.AppRoles?.Update(infoRole);

#pragma warning disable CS8602
                return await _dbcontext?.SaveChangesAsync();
#pragma warning restore CS8602

            }
            catch
            {
                return -1;
            }
        }
    }
}
