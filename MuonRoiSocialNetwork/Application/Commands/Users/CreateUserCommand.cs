using AutoMapper;
using BaseConfig.EntityObject.Entity;
using BaseConfig.Exeptions;
using BaseConfig.MethodResult;
using ConnectVN.Social_Network.Users;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using MuonRoiSocialNetwork.Application.Commands.Base;
using MuonRoiSocialNetwork.Common.Models.Users;
using MuonRoiSocialNetwork.Common.Requests.Users;
using MuonRoiSocialNetwork.Domains.Interfaces;
using MuonRoiSocialNetwork.Infrastructure.Extentions.Mail;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace MuonRoiSocialNetwork.Application.Commands.Users
{
    /// <summary>
    /// Command for user
    /// </summary>
    public class CreateUserCommand : CreateUserCommandModel, IRequest<MethodResult<UserModel>>
    { }
    /// <summary>
    /// Handler create user
    /// </summary>
    public class CreateUserCommandHandler : BaseCommandHandler, IRequestHandler<CreateUserCommand, MethodResult<UserModel>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="userRepository"></param>
        /// <param name="configuration"></param>
        /// <param name="emailService"></param>
        public CreateUserCommandHandler(IMapper mapper,
            IUserRepository userRepository,
            IConfiguration configuration,
            IEmailService emailService) : base(mapper, configuration)
        {
            _userRepository = userRepository;
            _emailService = emailService;
        }
        /// <summary>
        /// Handle register
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<MethodResult<UserModel>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            MethodResult<UserModel> methodResult = new();
            try
            {
                #region Validation
                AppUser newUser = _mapper.Map<AppUser>(request);
                newUser.LastLogin = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                if (!newUser.IsValid())
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddResultFromErrorList(newUser.ErrorMessages);
                    return methodResult;
                }
                string pwdPattern = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$";
                bool isComplex = Regex.IsMatch(request.PasswordHash, pwdPattern);
                if (!isComplex)
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USR17C),
                        new[] { Helpers.GenerateErrorResult(nameof(request.PasswordHash), request.PasswordHash) }
                    );
                    return methodResult;
                }
                #endregion

                #region Check is exist user
                bool appUser = await _userRepository.ExistUserByUsernameAsync(newUser.UserName);
                if (appUser)
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USR13C),
                        new[] { Helpers.GenerateErrorResult(nameof(newUser.UserName), newUser.UserName) }
                    );
                    return methodResult;
                }
                #endregion

                #region Genarate salt and password
                newUser.Salt = GenarateSalt();
                newUser.PasswordHash = HashPassword(request.PasswordHash, newUser.Salt);
                #endregion

                #region Create new user
                if (await _userRepository.CreateNewUserAsync(newUser) <= 0)
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USR29C),
                        new[] { Helpers.GenerateErrorResult(nameof(newUser.UserName), newUser.UserName) }
                    );
                    return methodResult;
                }
                #endregion

                #region  Send mail comfirm
                await GenerateEmailConfirmationTokenAsync(newUser);
                #endregion

                #region return info new user registed
                AppUser getCreatedUser = await _userRepository.GetByGuidAsync(newUser.Id);
                if (getCreatedUser == null)
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USR02C),
                        new[] { Helpers.GenerateErrorResult(nameof(newUser.UserName), newUser.UserName) }
                    );
                    return methodResult;
                };

                UserModel resultUser = _mapper.Map<UserModel>(getCreatedUser);

                methodResult.Result = resultUser;
                methodResult.StatusCode = StatusCodes.Status200OK;

                #endregion

                return methodResult;
            }
            catch (CustomException ex)
            {
                methodResult.AddResultFromErrorList(ex.ErrorMessages);
            }
            catch (Exception ex)
            {
                methodResult.AddErrorMessage(Helpers.GetExceptionMessage(ex), ex.StackTrace ?? "");
            }
            return methodResult;
        }
        /// <summary>
        /// Genarate mail and token confirm user register
        /// </summary>
        /// <param name="identityUser"></param>
        /// <returns></returns>
        public async Task GenerateEmailConfirmationTokenAsync(AppUser identityUser)
        {

            var symmetricKey = new SymmetricSecurityKey(Convert.FromBase64String(_configuration.GetSection("Application:SERECT").Value.ToString()));
            var tokenHandler = new JwtSecurityTokenHandler();
            var myIssuer = _configuration.GetSection("Application:ENV_CONNECTIONSTRING").Value.ToString();
            var myAudience = _configuration.GetSection("Application:AppDomain").Value.ToString();
            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, identityUser.UserName),
                    new Claim(ClaimTypes.Email, identityUser.Email),
                    new Claim(ClaimTypes.MobilePhone,identityUser.PhoneNumber)
                }),

                Expires = now.AddMinutes(Convert.ToInt32(5)),
                Issuer = myIssuer,
                Audience = myAudience,
                SigningCredentials = new SigningCredentials(symmetricKey,
                SecurityAlgorithms.HmacSha256Signature)
            };

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            string token = tokenHandler.WriteToken(stoken);
            if (!string.IsNullOrEmpty(token))
            {
                await SendEmailConfirmationEmail(identityUser, token);
            }
        }
        private async Task SendEmailConfirmationEmail(AppUser user, string token)
        {
            string appDomain = _configuration.GetSection("Application:AppDomain").Value;
            string confirmationLink = _configuration.GetSection("Application:EmailConfirmation").Value;

            UserEmailOptions options = new()
            {
                ToEmails = new List<string>() { user.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}", user.UserName),
                    new KeyValuePair<string, string>("{{Link}}",
                        string.Format(appDomain + confirmationLink, user.Id, token))
                }
            };

            await _emailService.SendEmailForEmailConfirmation(options);
        }
    }
}
