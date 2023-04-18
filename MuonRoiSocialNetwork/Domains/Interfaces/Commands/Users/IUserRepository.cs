using MuonRoi.Social_Network.Users;

namespace MuonRoiSocialNetwork.Domains.Interfaces.Commands.Users
{
    /// <summary>
    /// UserRepository Interface
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Confirmed Email
        /// </summary>
        /// <param name="checkUser"></param>
        /// <returns></returns>
        Task<int> ConfirmedEmail(AppUser checkUser);

        /// <summary>
        /// Create new user no role
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        Task<int> CreateNewUserAsync(AppUser newUser);
        /// <summary>
        /// Delete user for guid
        /// </summary>
        /// <param name="guidUser"></param>
        /// <returns></returns>
        Task<int> DeleteUserAsync(Guid guidUser);
        /// <summary>
        /// Check is exist user by guid
        /// </summary>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        Task<bool> ExistUserByGuidAsync(Guid userGuid);

        /// <summary>
        /// Check user is exist? by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<bool> ExistUserByUsernameAsync(string username);
        /// <summary>
        /// Change password forgot
        /// </summary>
        /// <param name="userGuid"></param>
        /// <param name="salt"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        Task<bool> UpdatePassworAsync(Guid userGuid, string salt, string passwordHash);

        /// <summary>
        /// Update info user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="salt"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        Task<int> UpdateUserAsync(AppUser user, string? salt = null, string? passwordHash = null);
    }
}
