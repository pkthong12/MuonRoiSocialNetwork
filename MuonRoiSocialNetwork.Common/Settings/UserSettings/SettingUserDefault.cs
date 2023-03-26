namespace MuonRoiSocialNetwork.Common.Settings.UserSettings
{
    public static class SettingUserDefault
    {
        /// <summary>
        /// The numbers maximum of fail login attemps for current user
        /// </summary>
        public static int loginAttempDefault = 5;
        /// <summary>
        /// The numbers maximum character when fotgot password 
        /// </summary>
        public static int genarePasswordDefaultCharacter = 8;
        /// <summary>
        /// The numbers maximum character of refreshToken
        /// </summary>
        public static int genareRefreshToken = 32;
        /// <summary>
        /// ExpiryDay of refresh token
        /// </summary>
        public static int refreshTokenExpiryDay = 7;
        /// <summary>
        /// The number hour when convert datetime utc to asia 
        /// </summary>
        public static int hourAsia = 7;
        /// <summary>
        /// Expiry time life of access token
        /// </summary>
        public static int minuteExpitryLogin = 60;
        /// <summary>
        /// Expiry time life of token email
        /// </summary>
        public static int minuteExpitryConfirmEmail = 5;
        /// <summary>
        /// The character use random password
        /// </summary>
        public static string alphabet = "ab$%cdefoGHvwBCiDEghi!$%^*jklkmnpuFx789y@zAK@#LMNO^^STUVPQqr^&XYRWZ1IJ456st0";
        /// <summary>
        /// Group default
        /// </summary>
        public static int groupDefault = 1;
        /// <summary>
        /// Role default
        /// </summary>
        public static Guid roleDefault = new Guid("5EF7D163-8249-445C-8895-4EB97329AF7E");
    }
}
