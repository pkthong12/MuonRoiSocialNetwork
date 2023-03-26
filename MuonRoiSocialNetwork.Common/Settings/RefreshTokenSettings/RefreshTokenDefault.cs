namespace MuonRoiSocialNetwork.Common.Settings.RefreshTokenSettings
{
    public static class RefreshTokenDefault
    {
        /// <summary>
        /// Key user info when login
        /// </summary>
        public static string keyUserModelResponse = "UserModelResponse";
        /// <summary>
        /// Life time expiration
        /// </summary>
        public static TimeSpan expirationTime = TimeSpan.FromMinutes(61);
        /// <summary>
        /// Life time slidingExpiration
        /// </summary>
        public static TimeSpan slidingExpiration = TimeSpan.FromMinutes(70);
    }
}
