using AutoMapper;
using BaseConfig.MethodResult;
using MuonRoi.Social_Network.Roles;
using MuonRoi.Social_Network.Users;
using MuonRoiSocialNetwork.Domains.DomainObjects.Groups;
using MuonRoiSocialNetwork.Domains.Interfaces.Queries;
using MuonRoiSocialNetwork.Infrastructure;
using Microsoft.EntityFrameworkCore;
using BaseConfig.EntityObject.Entity;
using MuonRoiSocialNetwork.Common.Models.Users.Response;
using MuonRoiSocialNetwork.Common.Models.Users.Base.Response;
using BaseConfig.Extentions.Datetime;
using BaseConfig.Extentions.Image;
using MuonRoiSocialNetwork.Common.Settings.UserSettings;

namespace MuonRoiSocialNetwork.Application.Queries
{
    /// <summary>
    /// Handle user querys
    /// </summary>
    public class UserQueries : IUserQueries
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly MuonRoiSocialNetworkDbContext _dbcontext;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        /// <param name="configuration"></param>
        public UserQueries(MuonRoiSocialNetworkDbContext dbContext, IMapper mapper, IConfiguration configuration)
        {
            _dbcontext = dbContext;
            _mapper = mapper;
            _configuration = configuration;
        }
        /// <summary>
        /// Handle get user by guid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AppUser> GetByGuidAsync(Guid id)
#pragma warning disable CS8604
            => await _dbcontext.AppUsers
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.Id.Equals(id) && !x.IsDeleted);
#pragma warning restore CS8604
        /// <summary>
        /// handle get user by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<AppUser> GetByUsernameAsync(string username)
#pragma warning disable CS8604
        => await _dbcontext.AppUsers
                       .AsNoTracking()
                       .FirstOrDefaultAsync(x => x.UserName.Equals(username) && !x.IsDeleted);
#pragma warning restore CS8604
        /// <summary>
        /// handle get user by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns>UserModel</returns>
        public async Task<MethodResult<BaseUserResponse>> GetUserModelBynameAsync(string username)
        {
            MethodResult<BaseUserResponse> methodResult = new();
#pragma warning disable CS8604
            AppUser? appUser = await _dbcontext.AppUsers
                       .AsNoTracking()
                       .FirstOrDefaultAsync(x => x.UserName.Equals(username) && !x.IsDeleted);
#pragma warning restore CS8604
            if (appUser == null)
            {
                methodResult.StatusCode = StatusCodes.Status400BadRequest;
                methodResult.AddApiErrorMessage(
                    nameof(EnumUserErrorCodes.USR02C),
                    new[] { Helpers.GenerateErrorResult(nameof(username), username ?? "") }
                );
                return methodResult;
            }
#pragma warning disable CS8604
            List<AppRole>? roles = await _dbcontext.AppRoles.Where(x => !x.IsDeleted).ToListAsync();
#pragma warning restore CS8604
#pragma warning disable CS8604 
            List<GroupUserMember>? groups = await _dbcontext.GroupUserMembers.Where(x => !x.IsDeleted).ToListAsync();
#pragma warning restore CS8604
            if (!roles.Any() || !groups.Any())
            {
                methodResult.StatusCode = StatusCodes.Status400BadRequest;
                methodResult.AddApiErrorMessage(
                    nameof(EnumUserErrorCodes.USRC46C),
                    new[] { Helpers.GenerateErrorResult(nameof(EnumUserErrorCodes.USRC46C), nameof(EnumUserErrorCodes.USRC46C) ?? "") }
                );
                return methodResult;
            }
            var userRole = (from role in roles
                            join gr in groups
                            on role.GroupId equals gr.Id
                            where gr.Id == appUser.GroupId
                            select new { role, gr }).FirstOrDefault();
            methodResult.Result = _mapper.Map<UserModelResponse>(appUser);
            methodResult.Result.RoleName = userRole?.role.Name ?? "";
            methodResult.Result.GroupName = userRole?.gr.GroupName ?? "";
            methodResult.Result.CreateDate = DateTimeExtensions.TimeStampToDateTime(appUser.CreatedDateTS ?? 0).AddHours(SettingUserDefault.hourAsia);
            methodResult.Result.UpdateDate = DateTimeExtensions.TimeStampToDateTime(appUser.UpdatedDateTS ?? 0).AddHours(SettingUserDefault.hourAsia);
            methodResult.Result.Avatar = HandlerImg.GetLinkImg(_configuration, methodResult.Result.Avatar ?? "");
            return methodResult;
        }
        /// <summary>
        /// Handle get user by guid
        /// </summary>
        /// <param name="guidUser"></param>
        /// <returns></returns>
        public async Task<MethodResult<BaseUserResponse>> GetUserModelByGuidAsync(Guid guidUser)
        {
            MethodResult<BaseUserResponse> methodResult = new();
#pragma warning disable CS8604
            AppUser? appUser = await _dbcontext.AppUsers
                       .AsNoTracking()
                       .FirstOrDefaultAsync(x => x.Id.Equals(guidUser) && !x.IsDeleted);
#pragma warning restore CS8604
            if (appUser == null)
            {
                methodResult.StatusCode = StatusCodes.Status400BadRequest;
                methodResult.AddApiErrorMessage(
                    nameof(EnumUserErrorCodes.USR02C),
                    new[] { Helpers.GenerateErrorResult(nameof(guidUser), guidUser) }
                );
                return methodResult;
            }
#pragma warning disable CS8604
            List<AppRole>? roles = await _dbcontext.AppRoles.Where(x => !x.IsDeleted).ToListAsync();
#pragma warning restore CS8604
#pragma warning disable CS8604 
            List<GroupUserMember>? groups = await _dbcontext.GroupUserMembers.Where(x => !x.IsDeleted).ToListAsync();
#pragma warning restore CS8604
            if (!roles.Any() || !groups.Any())
            {
                methodResult.StatusCode = StatusCodes.Status400BadRequest;
                methodResult.AddApiErrorMessage(
                    nameof(EnumUserErrorCodes.USRC46C),
                    new[] { Helpers.GenerateErrorResult(nameof(EnumUserErrorCodes.USRC46C), nameof(EnumUserErrorCodes.USRC46C) ?? "") }
                );
                return methodResult;
            }
            var userRole = (from role in roles
                            join gr in groups
                            on role.GroupId equals gr.Id
                            where gr.Id == appUser.GroupId
                            select new { role, gr }).FirstOrDefault();
            methodResult.Result = _mapper.Map<UserModelResponse>(appUser);
            methodResult.Result.RoleName = userRole?.role.Name ?? "";
            methodResult.Result.GroupName = userRole?.gr.GroupName ?? "";
            methodResult.Result.CreateDate = DateTimeExtensions.TimeStampToDateTime(appUser.CreatedDateTS ?? 0).AddHours(SettingUserDefault.hourAsia);
            methodResult.Result.UpdateDate = DateTimeExtensions.TimeStampToDateTime(appUser.UpdatedDateTS ?? 0).AddHours(SettingUserDefault.hourAsia);
            methodResult.Result.Avatar = HandlerImg.GetLinkImg(_configuration, methodResult.Result.Avatar ?? "");
            return methodResult;
        }
        /// <summary>
        /// Get user by email handle
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<AppUser> GetUserByEmailAsync(string email)
#pragma warning disable CS8604
            => await _dbcontext.AppUsers.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email && !x.IsDeleted).ConfigureAwait(false);
#pragma warning restore CS8604
    }
}
