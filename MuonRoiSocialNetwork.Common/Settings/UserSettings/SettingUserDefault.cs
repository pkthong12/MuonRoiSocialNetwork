namespace MuonRoiSocialNetwork.Common.Settings.UserSettings
{
    public static class SettingUserDefault
    {
        /// <summary>
        /// The numbers maximum of fail login attemps for current user
        /// </summary>
        public const int loginAttempDefault = 5;
        /// <summary>
        /// The numbers maximum character when fotgot password 
        /// </summary>
        public const int genarePasswordDefaultCharacter = 8;
        /// <summary>
        /// The numbers maximum character of refreshToken
        /// </summary>
        public const int genareRefreshToken = 32;
        /// <summary>
        /// ExpiryDay of refresh token
        /// </summary>
        public const int refreshTokenExpiryDay = 7;
        /// <summary>
        /// The number hour when convert datetime utc to asia 
        /// </summary>
        public const int hourAsia = 7;
        /// <summary>
        /// Expiry time life of access token
        /// </summary>
        public const int minuteExpitryLogin = 15;
        /// <summary>
        /// Expiry time life of token email
        /// </summary>
        public const int minuteExpitryConfirmEmail = 5;
        /// <summary>
        /// The character use random password
        /// </summary>
        public const string alphabet = "ab$%cdefoGHvwBCiDEghi!$%^*jklkmnpuFx789y@zAK@#LMNO^^STUVPQqr^&XYRWZ1IJ456st0";
        /// <summary>
        /// Group default
        /// </summary>
        public const int groupDefault = 1;
        /// <summary>
        /// Role default
        /// </summary>
        public static Guid roleDefault = new("5EF7D163-8249-445C-8895-4EB97329AF7E");
        /// <summary>
        /// Number max when request send mail veritification
        /// </summary>
        public const int maxNumberRequestSendMail = 3;
        /// <summary>
        /// Number max when login fail
        /// </summary>
        public const int maxNumberLogin = 5;
    }
}
