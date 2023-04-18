using BaseConfig.BaseDbContext.BaseQuery;
using BaseConfig.Infrashtructure;
using Microsoft.EntityFrameworkCore;
using MuonRoi.Social_Network.Roles;
using MuonRoiSocialNetwork.Domains.Interfaces.Queries.GroupAndRoles;
using MuonRoiSocialNetwork.Infrastructure;

namespace MuonRoiSocialNetwork.Application.Queries.GroupAndRoles
{
    /// <summary>
    /// Handler queries
    /// </summary>
    public class GroupQueries : BaseQuery<GroupUserMember>, IGroupQueries
    {
        private readonly MuonRoiSocialNetworkDbContext _dbcontext;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="authContext"></param>
        public GroupQueries(MuonRoiSocialNetworkDbContext dbContext, AuthContext authContext) : base(dbContext, authContext)
        {
            _dbcontext = dbContext;
        }
        /// <summary>
        /// Handler get group by name
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public async Task<bool> GetByNameAsync(string groupName)
        {
            if (string.IsNullOrEmpty(groupName))
            {
                return false;
            }
#pragma warning disable CS8604
            return await _dbcontext.GroupUserMembers.AnyAsync(x => x.GroupName == groupName); ;
#pragma warning restore CS8604
        }
    }
}
