using MuonRoiSocialNetwork.Domains.DomainObjects.Users;

namespace MuonRoiSocialNetwork.Domains.Interfaces.Commands.Token
{
    /// <summary>
    /// Interface decalare function handle refresh token table
    /// </summary>
    public interface IRefreshtokenRepository
    {
        /// <summary>
        /// create data to refresh token table
        /// </summary>
        /// <param name="userLoggin"></param>
        /// <returns></returns>
        Task<int> CreateRefreshTokenAsync(UserLogin userLoggin);
        /// <summary>
        /// Revoke data to refresh token table
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> RevokeRefreshTokenAsync(Guid id);
        /// <summary>
        /// Get data to refresh token table
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Dictionary<string, string[]>> GetInfoRefreshTokenAsync(Guid id);
    }
}
