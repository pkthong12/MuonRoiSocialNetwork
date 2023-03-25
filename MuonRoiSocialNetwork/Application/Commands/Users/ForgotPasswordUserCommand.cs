﻿using AutoMapper;
using BaseConfig.EntityObject.Entity;
using BaseConfig.MethodResult;
using MediatR;
using MuonRoi.Social_Network.Users;
using MuonRoiSocialNetwork.Application.Commands.Base;
using MuonRoiSocialNetwork.Domains.Interfaces.Commands;
using MuonRoiSocialNetwork.Domains.Interfaces.Queries;
using MuonRoiSocialNetwork.Infrastructure.Extentions.Mail;
using MuonRoiSocialNetwork.Infrastructure.Services;
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
        private readonly ILogger<ForgotPasswordUserCommandHandler> _logger;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="configuration"></param>
        /// <param name="userQueries"></param>
        /// <param name="userRepository"></param>
        /// <param name="emailService"></param>
        /// <param name="mediator"></param>
        /// <param name="logger"></param>
        public ForgotPasswordUserCommandHandler(IMapper mapper, IConfiguration configuration, IUserQueries userQueries, IUserRepository userRepository, IEmailService emailService, IMediator mediator, ILoggerFactory logger) : base(mapper, configuration, userQueries, userRepository)
        {
            _emailService = emailService;
            _mediator = mediator;
            _logger = logger.CreateLogger<ForgotPasswordUserCommandHandler>();
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
                string rawPassword = RandomString(SettingUserDefault.genarePasswordDefaultCharacter);
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
                _logger.LogError($" -->(FORGOT PASSWORD) STEP CHECK {"Exception".ToUpper()} --> EXEPTION: {ex}");
                _logger.LogError($" -->(FORGOT PASSWORD) STEP CHECK {"Exception".ToUpper()} --> EXEPTION{" StackTrace".ToUpper()}: {ex.StackTrace}");
                methodResult.AddErrorMessage(Helpers.GetExceptionMessage(ex), ex.StackTrace ?? "");
                return methodResult;
            }
            methodResult.Result = true;
            methodResult.StatusCode = StatusCodes.Status200OK;
            return methodResult;
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
