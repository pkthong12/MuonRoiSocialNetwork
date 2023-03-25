using AutoMapper;
using BaseConfig.EntityObject.Entity;
using BaseConfig.MethodResult;
using MediatR;
using MuonRoi.Social_Network.Users;
using MuonRoiSocialNetwork.Application.Commands.Base;
using System.Security.Cryptography;
using MuonRoiSocialNetwork.Domains.Interfaces.Commands;
using MuonRoiSocialNetwork.Domains.Interfaces.Queries;
using MuonRoiSocialNetwork.Infrastructure.Extentions.Mail;
using MuonRoiSocialNetwork.Infrastructure.Services;
using System.Text;
using System.ComponentModel;
using System;
using Microsoft.Extensions.Primitives;
using System.Diagnostics;
using MuonRoiSocialNetwork.Common.Models.Users.Base.Response;
using MuonRoiSocialNetwork.Common.Settings.UserSettings;

namespace MuonRoiSocialNetwork.Application.Commands.Users
{
    /// <summary>
    /// Forgot password command request
    /// </summary>
    public class ForgotPasswordUserCommand : IRequest<MethodResult<bool>>
    {
        /// <summary>
        /// Username of user get password
        /// </summary>
        public string? Username { get; set; }
    }
    /// <summary>
    /// Handler forgot password
    /// </summary>
    public class ForgotPasswordUserCommandHandler : BaseCommandHandler, IRequestHandler<ForgotPasswordUserCommand, MethodResult<bool>>
    {
        private readonly IMediator _mediator;
        private readonly IEmailService _emailService;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="configuration"></param>
        /// <param name="userQueries"></param>
        /// <param name="userRepository"></param>
        /// <param name="emailService"></param>
        /// <param name="mediator"></param>
        public ForgotPasswordUserCommandHandler(IMapper mapper, IConfiguration configuration, IUserQueries userQueries, IUserRepository userRepository, IEmailService emailService, IMediator mediator) : base(mapper, configuration, userQueries, userRepository)
        {
            _emailService = emailService;
            _mediator = mediator;
        }
        /// <summary>
        /// Handler function
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<MethodResult<bool>> Handle(ForgotPasswordUserCommand request, CancellationToken cancellationToken)
        {
            MethodResult<bool> methodResult = new();
            try
            {
                #region Check vaild username
                if (request.Username == null)
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USR03C),
                        new[] { Helpers.GenerateErrorResult(nameof(EnumUserErrorCodes.USR03C), nameof(EnumUserErrorCodes.USR03C) ?? "") }
                    );
                    methodResult.Result = false;
                    return methodResult;
                }
                #endregion

                #region Check user is exist by username
                AppUser existUser = await _userQueries.GetByUsernameAsync(request.Username);
                if (existUser == null)
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USR02C),
                        new[] { Helpers.GenerateErrorResult(nameof(request.Username), nameof(request.Username) ?? "") }
                    );
                    methodResult.Result = false;
                    return methodResult;
                }
                #endregion

                #region Send new password
                string newSalt = GenarateSalt();
                string rawPassword = RandomString(LoginAttemp.genarePasswordDefaultCharacter);
                string newPassword = HashPassword(rawPassword, newSalt);
                await SendEmailConfirmationEmail(existUser, rawPassword);
                #endregion

                #region update info user include password and salf | status | reason
                UpdateInformationCommand updateInformationCommand = new()
                {
                    NewSalf = newSalt,
                    NewPassword = newPassword,
                    AccountStatus = EnumAccountStatus.IsRenew,
                    Reason = EnumAccountStatus.IsRenew.ToString(),
                };
                _mapper.Map(existUser, updateInformationCommand);
                MethodResult<BaseUserResponse> methodResultUpdateInfo = await _mediator.Send(updateInformationCommand, cancellationToken).ConfigureAwait(false);
                if (methodResultUpdateInfo.StatusCode == StatusCodes.Status400BadRequest)
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USRC42C),
                        new[] { Helpers.GenerateErrorResult(nameof(request.Username), nameof(request.Username) ?? "") }
                    );
                    methodResult.Result = false;
                    return methodResult;
                }
                #endregion
            }
            catch (Exception ex)
            {
                methodResult.StatusCode = StatusCodes.Status400BadRequest;
                methodResult.Result = false;
                methodResult.AddErrorMessage(Helpers.GetExceptionMessage(ex), ex.StackTrace ?? "");
                return methodResult;
            }
            methodResult.Result = true;
            methodResult.StatusCode = StatusCodes.Status200OK;
            return methodResult;
        }
        private static string RandomString(int length)
        {
            if (length < 0) { return null; }
            const string alphabet = "ab$%cdefoGHvwBCDEghi!$%^*jklmnpuFx789y@#zAK@#LMNO^^STUVPQqr^&XYRWZ1IJ456st0";
            StringBuilder stringBuilder = new();
            using (var rng = RandomNumberGenerator.Create())
            {
                int count = (int)Math.Ceiling(Math.Log(alphabet.Length, 2) / 8.0);
                Debug.Assert(count <= sizeof(uint));
                int offset = BitConverter.IsLittleEndian ? 0 : sizeof(uint) - count;
                int max = (int)(Math.Pow(2, count * 8) / alphabet.Length) * alphabet.Length;
                byte[] uintBuffer = new byte[sizeof(uint)];

                while (stringBuilder.Length < length)
                {
                    rng.GetBytes(uintBuffer, offset, count);
                    uint num = BitConverter.ToUInt32(uintBuffer, 0);
                    if (num < max)
                    {
                        stringBuilder.Append(alphabet[(int)(num % alphabet.Length)]);
                    }
                }

            }
            return stringBuilder.ToString();
        }
        private async Task SendEmailConfirmationEmail(AppUser user, string newPass)
        {
            UserEmailOptions options = new()
            {
                ToEmails = new List<string>() { user.Email ?? "" },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}", user.UserName ?? ""),
                    new KeyValuePair<string, string>("{{Link}}",
                        string.Format(newPass))
                }
            };
            await _emailService.SendEmailForForgotPassword(options);
        }
    }
}
