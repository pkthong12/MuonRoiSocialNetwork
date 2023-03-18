using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MuonRoiSocialNetwork.Common.Models.Users.Response;
using MuonRoiSocialNetwork.Common.Settings.Appsettings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BaseConfig.JWT
{
    public class GenarateJwtToken
    {
        private readonly IConfiguration _configuration;
        public GenarateJwtToken(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenarateJwt(UserModelResponse User)
        {
            SymmetricSecurityKey symmetricKey = new SymmetricSecurityKey(Convert.FromBase64String(_configuration.GetSection(ConstAppSettings.APPLICATIONSERECT).Value));
            JwtSecurityTokenHandler tokenHandler = new();
            string? myIssuer = _configuration.GetSection(ConstAppSettings.ENV_SERECT).Value;
            string? myAudience = _configuration.GetSection(ConstAppSettings.APPLICATIONAPPDOMAIN).Value;
            DateTime now = DateTime.UtcNow;
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(new[]
                {   new Claim("name_user", User.Name ?? ""),
                    new Claim("username", User.Username ?? ""),
                    new Claim("user_id", User.Id.ToString()),
                    new Claim("email", User.Email ?? ""),
                    new Claim("group_id",User.GroupId.ToString())
                }),

                Expires = now.AddDays(30),
                Issuer = myIssuer,
                Audience = myAudience,
                SigningCredentials = new SigningCredentials(symmetricKey,
                SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken? stoken = tokenHandler.CreateToken(tokenDescriptor);
            string token = tokenHandler.WriteToken(stoken);
            return token;
        }
        public string GenarateJwt(UserModelResponse User, int expiresTime)
        {
            SymmetricSecurityKey symmetricKey = new(Convert.FromBase64String(_configuration.GetSection(ConstAppSettings.APPLICATIONSERECT).Value));
            JwtSecurityTokenHandler tokenHandler = new();
            string? myIssuer = _configuration.GetSection(ConstAppSettings.ENV_SERECT).Value;
            string? myAudience = _configuration.GetSection(ConstAppSettings.APPLICATIONAPPDOMAIN).Value;
            DateTime now = DateTime.UtcNow;
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("name_user", User.Name ?? ""),
                    new Claim("username", User.Username ?? ""),
                    new Claim("user_id", User.Id.ToString()),
                    new Claim("email", User.Email ?? ""),
                    new Claim("group_id",User.GroupId.ToString())
                }),

                Expires = now.AddMinutes(expiresTime),
                Issuer = myIssuer,
                Audience = myAudience,
                SigningCredentials = new SigningCredentials(symmetricKey,
                SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken? stoken = tokenHandler.CreateToken(tokenDescriptor);
            string token = tokenHandler.WriteToken(stoken);
            return token;
        }
    }
}
