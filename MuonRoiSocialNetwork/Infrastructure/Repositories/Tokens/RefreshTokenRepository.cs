using BaseConfig.Extentions.Datetime;
using Microsoft.EntityFrameworkCore;
using MuonRoiSocialNetwork.Common.Settings.UserSettings;
using MuonRoiSocialNetwork.Domains.DomainObjects.Users;
using MuonRoiSocialNetwork.Domains.Interfaces.Commands.Token;

namespace MuonRoiSocialNetwork.Infrastructure.Repositories.Token
{
    /// <summary>
    /// Repository of refresh Token
    /// </summary>
    public class RefreshTokenRepository : IRefreshtokenRepository
    {
        private readonly MuonRoiSocialNetworkDbContext _dbContext;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext"></param>
        public RefreshTokenRepository(MuonRoiSocialNetworkDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// Func handle insert data to refresh token table
        /// </summary>
        /// <param name="userLoggin"></param>
        /// <returns></returns>
        public async Task<int> CreateRefreshTokenAsync(UserLogin userLoggin)
        {
            try
            {
                if (userLoggin is null)
                {
                    return -1;
                }
                userLoggin.CreateDateTS = DateTime.UtcNow.GetTimeStamp(true);
                userLoggin.RefreshTokenExpiryTimeTS = DateTime.UtcNow.AddDays(SettingUserDefault.refreshTokenExpiryDay).GetTimeStamp(true);
                _dbContext.UserLoggins?.Add(userLoggin);
                return await _dbContext.SaveChangesAsync();
            }
            catch
            {
                return -1;
            }

        }
        /// <summary>
        /// Handle function revoke refresh token
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> RevokeRefreshTokenAsync(Guid id)
        {
            try
            {
#pragma warning disable CS8600
#pragma warning disable CS8604
                UserLogin userLoggin = await _dbContext.UserLoggins.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == id).ConfigureAwait(false);
#pragma warning restore CS8604
#pragma warning restore CS8600
                if (id == Guid.Empty)
                {
                    return -1;
                }
                if (userLoggin is null)
                {
                    return -1;
                }
                _dbContext.UserLoggins?.Remove(userLoggin);
                return await _dbContext.SaveChangesAsync();
            }
            catch { return -1; }
        }
        /// <summary>
        /// Handle function get info refresh token
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, string[]>> GetInfoRefreshTokenAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return new Dictionary<string, string[]> { { "false", new[] { "false" } } };
            }
#pragma warning disable CS8600
#pragma warning disable CS8604
            UserLogin userLoggin = await _dbContext.UserLoggins.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == id).ConfigureAwait(false);
#pragma warning restore CS8604
#pragma warning restore CS8600
            if (userLoggin is null)
            {
                return new Dictionary<string, string[]> { { "false", new[] { "false" } } };
            }
#pragma warning disable CS8620
            Dictionary<string, string[]> keyValuePairs = new()
            {
               { userLoggin.UserId.ToString(), new[] { userLoggin.KeySalt, userLoggin.RefreshToken,userLoggin.RefreshTokenExpiryTimeTS.ToString() } }
            };
#pragma warning restore CS8620
            return keyValuePairs;
        }
    }
}
