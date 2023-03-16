using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MuonRoiSocialNetwork.Common.Models.Users;
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
        public string GenarateJwt(UserModelRequest User)
        {
            SymmetricSecurityKey symmetricKey = new SymmetricSecurityKey(Convert.FromBase64String(_configuration.GetSection(ConstAppSettings.APPLICATIONSERECT).Value));
            JwtSecurityTokenHandler tokenHandler = new();
            string? myIssuer = _configuration.GetSection(ConstAppSettings.APPLICATIONENVCONNECTION).Value;
            string? myAudience = _configuration.GetSection(ConstAppSettings.APPLICATIONAPPDOMAIN).Value;
            DateTime now = DateTime.UtcNow;
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, User.Name ?? ""),
                    new Claim("Username", User.UserName ?? ""),
                    new Claim(ClaimTypes.Email, User.Email ?? ""),
                    new Claim("Role",User.GroupId.ToString())
                }),

                Expires = now.AddMinutes(Convert.ToInt32(5)),
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
