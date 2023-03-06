using ConnectVN.Social_Network.Users;

namespace MuonRoiSocialNetwork.Domains.Interfaces
{
    /// <summary>
    /// UserRepository Interface
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Create new user no role
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        Task<int> CreateNewUserAsync(AppUser newUser);

        /// <summary>
        /// Check user is exist? by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<bool> ExistUserByUsernameAsync(string username);
        /// <summary>
        /// Get user by guid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AppUser> GetByGuidAsync(Guid id);
    }
}
