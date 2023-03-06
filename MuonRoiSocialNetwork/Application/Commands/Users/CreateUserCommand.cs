using AutoMapper;
using BaseConfig.EntityObject.Entity;
using BaseConfig.Exeptions;
using BaseConfig.MethodResult;
using ConnectVN.Social_Network.Users;
using MediatR;
using MuonRoiSocialNetwork.Application.Commands.Base;
using MuonRoiSocialNetwork.Common.Models.Users;
using MuonRoiSocialNetwork.Common.Requests.Users;
using MuonRoiSocialNetwork.Domains.Interfaces;
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
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="userRepository"></param>
        public CreateUserCommandHandler(IMapper mapper, IUserRepository userRepository) : base(mapper, userRepository)
        {

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
                newUser.Salt = GenarateSalt();
                newUser.PasswordHash = HashPassword(request.PasswordHash, newUser.Salt);
                if (await _userRepository.CreateNewUserAsync(newUser) <= 0)
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USR29C),
                        new[] { Helpers.GenerateErrorResult(nameof(newUser.UserName), newUser.UserName) }
                    );
                    return methodResult;
                }
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
                return methodResult;
                #endregion
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
    }
}
