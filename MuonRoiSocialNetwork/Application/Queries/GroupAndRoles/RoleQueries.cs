using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MuonRoiSocialNetwork.Common.Models.GroupAndRoles.Base.Response;
using MuonRoiSocialNetwork.Domains.DomainObjects.Groups;
using MuonRoiSocialNetwork.Domains.Interfaces.Queries.GroupAndRoles;
using MuonRoiSocialNetwork.Infrastructure;

namespace MuonRoiSocialNetwork.Application.Queries.GroupAndRoles
{
    /// <summary>
    /// Handler role queries
    /// </summary>
    public class RoleQueries : IRoleQueries
    {
        private readonly MuonRoiSocialNetworkDbContext _dbcontext;
        private readonly IMapper _mapper;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbcontext"></param>
        /// <param name="mapper"></param>
        public RoleQueries(MuonRoiSocialNetworkDbContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }
        /// <summary>
        /// Get role by guid
        /// </summary>
        /// <param name="roleGuid"></param>
        /// <returns></returns>
        public async Task<RoleInitialBaseResponse> GetRoleByGuidAsync(Guid roleGuid)
        {
            if (roleGuid == Guid.Empty)
            {
                return null;
            }
#pragma warning disable CS8602
            AppRole? roleInfo = await _dbcontext?.AppRoles?.FirstOrDefaultAsync(x => x.Id == roleGuid);
#pragma warning restore CS8602
            if (roleInfo == null)
            {
                return null;
            }
            RoleInitialBaseResponse returnInfo = _mapper.Map<RoleInitialBaseResponse>(roleInfo);
            return returnInfo;
        }
        /// <summary>
        /// Get role by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<bool> GetRoleByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }
#pragma warning disable CS8604
            return await _dbcontext.AppRoles.AnyAsync(x => x.Name == name); ;
#pragma warning restore CS8604
        }
    }
}
