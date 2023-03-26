using MuonRoi.Social_Network.Users;

namespace MuonRoiSocialNetwork.Domains.DomainObjects.Users
{
    /// <summary>
    /// Userloggin table
    /// </summary>
    public class UserLogin
    {
        /// <summary>
        /// UserId
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// Refresh Token
        /// </summary>
        public string? RefreshToken { get; set; }
        /// <summary>
        /// Key decrypt token
        /// </summary>
        public string? KeySalt { get; set; }
        /// <summary>
        /// Refresh Token ExpiryTime
        /// </summary>
        public double? RefreshTokenExpiryTimeTS { get; set; }
        /// <summary>
        /// CreateDate
        /// </summary>
        public double? CreateDateTS { get; set; }
        /// <summary>
        /// Foreign key
        /// </summary>
        public AppUser? AppUser { get; set; }
    }
}
