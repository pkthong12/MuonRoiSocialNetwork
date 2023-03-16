using AutoMapper;
using BaseConfig.MethodResult;
using MuonRoi.Social_Network.Roles;
using MuonRoi.Social_Network.Users;
using MuonRoiSocialNetwork.Common.Models.Users;
using MuonRoiSocialNetwork.Domains.DomainObjects.Groups;
using MuonRoiSocialNetwork.Domains.Interfaces.Queries;
using MuonRoiSocialNetwork.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace MuonRoiSocialNetwork.Application.Queries
{
    /// <summary>
    /// Handle user querys
    /// </summary>
    public class UserQueries : IUserQueries
    {
        private readonly IMapper _mapper;
        private readonly MuonRoiSocialNetworkDbContext _dbcontext;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public UserQueries(MuonRoiSocialNetworkDbContext dbContext, IMapper mapper)
        {
            _dbcontext = dbContext;
            _mapper = mapper;
        }
        /// <summary>
        /// Handle get user by guid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AppUser> GetByGuidAsync(Guid id)
        {
            return await _dbcontext.Users
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.Id.Equals(id));
        }
        /// <summary>
        /// handle get user by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<AppUser> GetByUsernameAsync(string username)
        {
            return await _dbcontext.Users
                       .AsNoTracking()
                       .FirstOrDefaultAsync(x => x.UserName.Equals(username) && !x.IsDeleted);
        }
        /// <summary>
        /// handle get user by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns>UserModel</returns>
        public async Task<MethodResult<UserModelResponse>> GetUserModelBynameAsync(string username)
        {
            MethodResult<UserModelResponse> methodResult = new();
            AppUser? appUser = await _dbcontext.Users
                       .AsNoTracking()
                       .FirstOrDefaultAsync(x => x.UserName.Equals(username) && !x.IsDeleted);
            if (appUser != null)
            {
                List<AppRole> roles = await _dbcontext.AppRoles.Where(x => !x.IsDeleted).ToListAsync();
                GroupUserMember? roleUser = _dbcontext.GroupUserMembers.FirstOrDefault(x => x.AppUserKey.Equals(appUser.Id) && !x.IsDeleted);
                methodResult.Result = _mapper.Map<UserModelResponse>(appUser);
                methodResult.Result.RoleName = roles.FirstOrDefault(x => x.Id.Equals(roleUser?.AppRoleKey))?.Name ?? "";
                methodResult.Result.GroupName = roleUser?.GroupName ?? "";
            }
            return methodResult;
        }
        /// <summary>
        /// Handle get user by guid
        /// </summary>
        /// <param name="guidUser"></param>
        /// <returns></returns>
        public async Task<MethodResult<UserModelResponse>> GetUserModelByGuidAsync(Guid guidUser)
        {
            MethodResult<UserModelResponse> methodResult = new();
            AppUser? appUser = await _dbcontext.Users
                       .AsNoTracking()
                       .FirstOrDefaultAsync(x => x.Id.Equals(guidUser) && !x.IsDeleted);
            if (appUser != null)
            {
                List<AppRole> roles = await _dbcontext.AppRoles.Where(x => !x.IsDeleted).ToListAsync();
                GroupUserMember? roleUser = _dbcontext.GroupUserMembers.FirstOrDefault(x => x.AppUserKey.Equals(appUser.Id) && !x.IsDeleted);
                methodResult.Result = _mapper.Map<UserModelResponse>(appUser);
                methodResult.Result.RoleName = roles.FirstOrDefault(x => x.Id.Equals(roleUser?.AppRoleKey))?.Name ?? "";
                methodResult.Result.GroupName = roleUser?.GroupName ?? "";
            }
            return methodResult;
        }
    }
}
