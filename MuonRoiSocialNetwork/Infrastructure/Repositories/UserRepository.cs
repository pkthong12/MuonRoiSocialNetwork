﻿using MuonRoi.Social_Network.Users;
using Microsoft.EntityFrameworkCore;
using MuonRoiSocialNetwork.Domains.Interfaces.Commands;
using BaseConfig.Extentions;
using MuonRoi.Social_Network.Roles;

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
            return await _dbcontext.AppUsers.AsNoTracking()
                 .AnyAsync(x => x.UserName.Equals(username))
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
            AppUser userDelete = await _dbcontext.AppUsers.Where(x => x.Id.Equals(userGuid) && !x.IsDeleted).FirstOrDefaultAsync();
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
    }
}
