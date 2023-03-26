using MuonRoi.Social_Network.Users;
using Microsoft.EntityFrameworkCore;
using MuonRoiSocialNetwork.Domains.Interfaces.Commands;
using MuonRoi.Social_Network.Roles;
using BaseConfig.Extentions.Datetime;
using BaseConfig.Extentions.String;

namespace MuonRoiSocialNetwork.Infrastructure.Repositories
{
    /// <summary>
    /// Handler user
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly MuonRoiSocialNetworkDbContext _dbcontext;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext"></param>
        public UserRepository(MuonRoiSocialNetworkDbContext dbContext)
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
#pragma warning disable CS8604
            return await _dbcontext.AppUsers.AsNoTracking()
                 .AnyAsync(x => x.UserName.Equals(username))
                 .ConfigureAwait(false);
#pragma warning restore CS8604
        }
        /// <summary>
        /// Handle check user is exist ? by guid
        /// </summary>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        public async Task<bool> ExistUserByGuidAsync(Guid userGuid)
        {
#pragma warning disable CS8604
            return await _dbcontext.AppUsers.AsNoTracking()
                .AnyAsync(x => x.Id.Equals(userGuid))
                .ConfigureAwait(false);
#pragma warning restore CS8604
        }
        /// <summary>
        /// Handle create new user no role
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        public async Task<int> CreateNewUserAsync(AppUser newUser)
        {
            try
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
            catch
            {

                return -1;
            }

        }
        /// <summary>
        /// Handle confirmed email
        /// </summary>
        /// <param name="checkUser"></param>
        /// <returns></returns>
        public async Task<int> ConfirmedEmail(AppUser checkUser)
        {
            try
            {
                checkUser.EmailConfirmed = true;
                checkUser.Status = EnumAccountStatus.Confirmed;
                DateTime utcNow = DateTime.UtcNow;
                checkUser.UpdatedDateTS = utcNow.GetTimeStamp(includedTimeValue: true);
#pragma warning disable CS8602
                _dbcontext.AppUsers.Update(checkUser);
#pragma warning restore CS8602
                return await _dbcontext.SaveChangesAsync();
            }
            catch
            {

                return -1;
            }

        }
        /// <summary>
        /// Handle update info user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="salt"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        public async Task<int> UpdateUserAsync(AppUser user, string? salt, string? passwordHash)
        {
            try
            {
                DateTime utcNow = DateTime.UtcNow;
                user.UpdatedDateTS = utcNow.GetTimeStamp(includedTimeValue: true);
                if (salt != null && passwordHash != null)
                {
                    user.Salt = salt;
                    user.PasswordHash = passwordHash;
#pragma warning disable CS8602
                    _dbcontext.AppUsers.Update(user);
#pragma warning restore CS8602
                    return await _dbcontext.SaveChangesAsync();
                }
#pragma warning disable CS8602
                _dbcontext.AppUsers.Update(user);
#pragma warning restore CS8602
                return await _dbcontext.SaveChangesAsync();
            }
            catch
            {
                return -1;
            }

        }
        /// <summary>
        /// Handle delete user
        /// </summary>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        public async Task<int> DeleteUserAsync(Guid userGuid)
        {
            try
            {
#pragma warning disable CS8604
                AppUser? userDelete = await _dbcontext.AppUsers.Where(x => x.Id.Equals(userGuid) && !x.IsDeleted).FirstOrDefaultAsync();
#pragma warning restore CS8604
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
            catch
            {
                return -1;
            }

        }
        /// <summary>
        /// Handle change password
        /// </summary>
        /// <param name="userGuid"></param>
        /// <param name="salt"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        public async Task<bool> UpdatePassworAsync(Guid userGuid, string? salt, string? passwordHash)
        {
#pragma warning disable CS8604
            AppUser? appUser = await _dbcontext.AppUsers.FirstOrDefaultAsync(x => x.Id.Equals(userGuid)).ConfigureAwait(false);
#pragma warning restore CS8604
            if (appUser == null)
            {
                return false;
            }
            appUser.UpdatedDateTS = DateTime.UtcNow.GetTimeStamp(includedTimeValue: true);
            appUser.Salt = salt;
            appUser.PasswordHash = passwordHash;
            appUser.AccountStatus = EnumAccountStatus.None;
            _dbcontext.AppUsers.Update(appUser);
            int resultUpdate = await _dbcontext.SaveChangesAsync();
            if (resultUpdate <= 0)
            {
                return false;
            }
            return true;
        }
    }
}
