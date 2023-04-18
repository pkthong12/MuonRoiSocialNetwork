namespace MuonRoiSocialNetwork.Common.Settings.RefreshTokenSettings
{
    public static class RefreshTokenDefault
    {
        /// <summary>
        /// Key user info when login
        /// </summary>
        public const string keyUserModelResponseLogin = "ModelResponseLogin";
        /// <summary>
        /// Key user info when register
        /// </summary>
        public const string keyUserModelResponseRegister = "ModelResponseRegister";
        /// <summary>
        /// Life time expiration
        /// </summary>
        public static TimeSpan expirationTimeLogin = TimeSpan.FromMinutes(60);
        /// <summary>
        /// Life time slidingExpiration
        /// </summary>
        public static TimeSpan slidingExpirationLogin = TimeSpan.FromMinutes(65);
    }
}
