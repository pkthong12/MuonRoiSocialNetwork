using AutoMapper;
using BaseConfig.EntityObject.Entity;
using BaseConfig.Exeptions;
using BaseConfig.Extentions.Image;
using BaseConfig.MethodResult;
using MediatR;
using MuonRoi.Social_Network.Users;
using MuonRoiSocialNetwork.Application.Commands.Base;
using MuonRoiSocialNetwork.Common.Models.Users.Base.Request;
using MuonRoiSocialNetwork.Common.Models.Users.Base.Response;
using MuonRoiSocialNetwork.Domains.Interfaces.Commands;
using MuonRoiSocialNetwork.Domains.Interfaces.Queries;

namespace MuonRoiSocialNetwork.Application.Commands.Users
{
    /// <summary>
    /// Update user command
    /// </summary>
    public class UpdateInformationCommand : BaseUserRequest, IRequest<MethodResult<BaseUserResponse>>
    {
        /// <summary>
        /// Avatar raw
        /// </summary>
        public IFormFile? AvatarTemp { get; set; }
        /// <summary>
        /// New salf when fotgot password
        /// </summary>
        public string? NewSalf { get; set; }
        /// <summary>
        /// New password when fotgot password
        /// </summary>
        public string? NewPassword { get; set; }
        /// <summary>
        /// Status for account
        /// </summary>
        public EnumAccountStatus AccountStatus { get; set; }
        /// <summary>
        /// Reason set status for account
        /// </summary>
        public string? Reason { get; set; }
    }
    /// <summary>
    /// Handler update infor user
    /// </summary>
    public class UpdateInformationCommandHandler : BaseCommandHandler, IRequestHandler<UpdateInformationCommand, MethodResult<BaseUserResponse>>
    {
        private readonly ILogger<UpdateInformationCommandHandler> _logger;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="configuration"></param>
        /// <param name="userRepository"></param>
        /// <param name="userQueries"></param>
        /// <param name="logger"></param>
        public UpdateInformationCommandHandler(IMapper mapper,
            IConfiguration configuration, IUserRepository userRepository, IUserQueries userQueries, ILoggerFactory logger) : base(mapper, configuration, userQueries, userRepository)
        {
            _logger = logger.CreateLogger<UpdateInformationCommandHandler>();
        }
        /// <summary>
        /// Function Handle
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<MethodResult<BaseUserResponse>> Handle(UpdateInformationCommand request, CancellationToken cancellationToken)
        {
            MethodResult<BaseUserResponse> methodResult = new();
            try
            {
                #region Check is exist user
                AppUser userIsExist = await _userQueries.GetByUsernameAsync(request.UserName ?? "");
                if (userIsExist == null)
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USR02C),
                        new[] { Helpers.GenerateErrorResult(nameof(request.UserName), request.UserName ?? "") }
                    );
                    return methodResult;
                }
                #endregion

                #region Check is update email
                if (request.Email != userIsExist.Email)
                {
                    AppUser existingUser = await _userQueries
                    .GetUserByEmailAsync(request.Email ?? "")
                    .ConfigureAwait(false);
                    if (existingUser != null)
                    {
                        methodResult.StatusCode = StatusCodes.Status400BadRequest;
                        methodResult.AddApiErrorMessage(
                            nameof(EnumUserErrorCodes.USRC34C),
                            new[] { Helpers.GenerateErrorResult(nameof(request.Email), request.Email ?? "") }
                        );
                        return methodResult;
                    }
                }
                #endregion

                #region upload avatar

                if (request.AvatarTemp != null)
                {
                    Dictionary<string, string> result = await HandlerImg.UploadImgAsync(_configuration, request.AvatarTemp);
                    userIsExist.Avatar = result.Keys.FirstOrDefault();
                    if (result.Values.Equals("OK"))
                    {
                        methodResult.StatusCode = StatusCodes.Status400BadRequest;
                        methodResult.AddApiErrorMessage(
                            nameof(EnumUserErrorCodes.USRC41C),
                            new[] { Helpers.GenerateErrorResult(nameof(request.UserName), request.UserName ?? "") }
                        );
                        return methodResult;
                    }
                }
                #endregion

                #region Update info user
                _mapper.Map(request, userIsExist);
                userIsExist.Status = request.AccountStatus == EnumAccountStatus.None ? request.AccountStatus : userIsExist.AccountStatus;
                userIsExist.LockReason = request.Reason ?? userIsExist.LockReason;
                if (await _userRepository.UpdateUserAsync(userIsExist, request.NewSalf, request.NewPassword) < 1)
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USR29C),
                        new[] { Helpers.GenerateErrorResult(nameof(userIsExist.UserName), userIsExist.UserName ?? "") }
                    );
                    return methodResult;
                }
                #endregion

                #region Return info user updated
                AppUser inforResult = await _userQueries.GetByUsernameAsync(userIsExist.UserName ?? "");
                BaseUserResponse resultInforLoginUser = _mapper.Map<BaseUserResponse>(inforResult);
                methodResult.Result = resultInforLoginUser;
                methodResult.StatusCode = StatusCodes.Status200OK;
                #endregion

            }
            catch (CustomException ex)
            {
                methodResult.StatusCode = StatusCodes.Status400BadRequest;
                _logger.LogError($" -->(UPDATE INFO) STEP CHECK {"CustomException".ToUpper()} --> EXEPTION: {ex}");
#pragma warning disable CS8604 // Possible null reference argument.
                methodResult.AddResultFromErrorList(ex.ErrorMessages);
#pragma warning restore CS8604 // Possible null reference argument.
            }
            catch (Exception ex)
            {
                methodResult.StatusCode = StatusCodes.Status400BadRequest;
                _logger.LogError($" -->(UPDATE INFO) STEP CHECK {"Exception".ToUpper()} --> EXEPTION: {ex}");
                _logger.LogError($" -->(UPDATE INFO) STEP CHECK {"Exception".ToUpper()} --> EXEPTION{" StackTrace".ToUpper()}: {ex.StackTrace}");
                methodResult.AddErrorMessage(Helpers.GetExceptionMessage(ex), ex.StackTrace ?? "");
            }
            return methodResult;
        }

    }
}
