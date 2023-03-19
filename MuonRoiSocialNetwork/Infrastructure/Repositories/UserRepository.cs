using MuonRoi.Social_Network.Users;
using Microsoft.EntityFrameworkCore;
using MuonRoiSocialNetwork.Domains.Interfaces.Commands;
using MuonRoi.Social_Network.Roles;
using BaseConfig.Extentions.Datetime;
using BaseConfig.Extentions.String;
using MuonRoiSocialNetwork.Application.Commands.Base;
using AutoMapper;
using MuonRoiSocialNetwork.Domains.Interfaces.Queries;

namespace MuonRoiSocialNetwork.Infrastructure.Repositories
{
    /// <summary>
    /// Handler user
    /// </summary>
    public class UserRepository : BaseCommandHandler, IUserRepository
    {
        private readonly MuonRoiSocialNetworkDbContext _dbcontext;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        /// <param name="userRepository"></param>
        /// <param name="userQueries"></param>
        /// <param name="configuration"></param>
        public UserRepository(MuonRoiSocialNetworkDbContext dbContext, IMapper mapper,
            IUserRepository userRepository,
            IUserQueries userQueries,
            IConfiguration configuration) : base(mapper, configuration, userQueries, userRepository)
        {
            _dbcontext = dbContext;
        }
        /// <summary>
        /// Handle check user is exist? by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<bool> ExistUserByUsernameAsync(string username)
        {
            return await _dbcontext.AppUsers.AsNoTracking()
                 .AnyAsync(x => x.UserName.Equals(username))
                 .ConfigureAwait(false);
        }
        /// <summary>
        /// Handle check user is exist ? by guid
        /// </summary>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        public async Task<bool> ExistUserByGuidAsync(Guid userGuid)
        {
            return await _dbcontext.AppUsers.AsNoTracking()
                .AnyAsync(x => x.Id.Equals(userGuid))
                .ConfigureAwait(false);
        }
        /// <summary>
        /// Handle create new user no role
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        public async Task<int> CreateNewUserAsync(AppUser newUser)
        {
            FormatString.WithRegex(newUser.Name ?? "");
            FormatString.WithRegex(newUser.Surname ?? "");
            FormatString.WithRegex(newUser.Address ?? "");
            newUser.Status = EnumAccountStatus.UnConfirm;
            CheckDateTime.IsValidDateTime(newUser.BirthDate);
            newUser.Avatar ??= newUser.Avatar ?? "".Trim();
            newUser.GroupId = (int)EnumStaff.Staff;
            DateTime utcNow = DateTime.UtcNow;
            newUser.CreatedDateTS = utcNow.GetTimeStamp(includedTimeValue: true);
            newUser.UpdatedDateTS = utcNow.GetTimeStamp(includedTimeValue: true);
            _dbcontext.Add(newUser);
            return await _dbcontext.SaveChangesAsync();
        }
        /// <summary>
        /// Handle confirmed email
        /// </summary>
        /// <param name="checkUser"></param>
        /// <returns></returns>
        public async Task<int> ConfirmedEmail(AppUser checkUser)
        {
            checkUser.EmailConfirmed = true;
            checkUser.Status = EnumAccountStatus.Confirmed;
            DateTime utcNow = DateTime.UtcNow;
            checkUser.UpdatedDateTS = utcNow.GetTimeStamp(includedTimeValue: true);
            _dbcontext.AppUsers.Update(checkUser);
            return await _dbcontext.SaveChangesAsync();
        }
        /// <summary>
        /// Handle update info user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<int> UpdateUserAsync(AppUser user)
        {
            DateTime utcNow = DateTime.UtcNow;
            user.UpdatedDateTS = utcNow.GetTimeStamp(includedTimeValue: true);
            _dbcontext.AppUsers.Update(user);
            return await _dbcontext.SaveChangesAsync();
        }
        /// <summary>
        /// Handle delete user
        /// </summary>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        public async Task<int> DeleteUserAsync(Guid userGuid)
        {
#pragma warning disable CS8600
            AppUser userDelete = await _dbcontext.AppUsers.Where(x => x.Id.Equals(userGuid) && !x.IsDeleted).FirstOrDefaultAsync();
#pragma warning restore CS8600
            if (userDelete == null)
            {
                return -1;
            }
            DateTime utcNow = DateTime.UtcNow;
            userDelete.DeletedDateTS = utcNow.GetTimeStamp(includedTimeValue: true);
            userDelete.IsDeleted = true;
            _dbcontext.AppUsers.Update(userDelete);
            return await _dbcontext.SaveChangesAsync();
        }
        /// <summary>
        /// Handle change password
        /// </summary>
        /// <param name="confirmPassword"></param>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        public async Task<bool> UpdatePassworAsync(string confirmPassword, Guid userGuid)
        {
            AppUser? appUser = await _dbcontext.AppUsers.FirstOrDefaultAsync(x => x.Id.Equals(userGuid)).ConfigureAwait(false);
            if (appUser == null)
            {
                return false;
            }
            appUser.UpdatedDateTS = DateTime.UtcNow.GetTimeStamp(includedTimeValue: true);
            appUser.Salt = GenarateSalt();
            appUser.PasswordHash = HashPassword(confirmPassword ?? "", appUser.Salt);
            return true;
        }
    }
}
