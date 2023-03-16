using AutoMapper;
using BaseConfig.EntityObject.Entity;
using BaseConfig.Extentions;
using BaseConfig.MethodResult;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MuonRoi.Social_Network.Users;
using MuonRoiSocialNetwork.Application.Commands.Users;
using MuonRoiSocialNetwork.Application.Queries;
using MuonRoiSocialNetwork.Common.Models.Users;
using MuonRoiSocialNetwork.Infrastructure.Repositories;
using NPOI.SS.Formula.Functions;
using Shouldly;

namespace MuonRoiSocialNetwork.Test.Command
{
    public class AuthUserCommandTest
    {
        private readonly MockDataBase _baseData = new();
        private readonly IMapper _mapper;
        public readonly UserRepository _user;
        public readonly UserQueries _userQueries;
        private readonly IConfiguration _config;
        public AuthUserCommandTest()
        {
            _user = _baseData._userRepoBase;
            _mapper = _baseData._maperBase;
            _config = _baseData._configBase;
            _userQueries = _baseData._userQueriesBase;
        }
        [Fact]
        public async Task LoginSuccess()
        {
            AuthUserCommand user = new()
            {
                Username = "test2",
                Password = "1234567Az*99",
            };
            AuthUserCommandHandler handler = new(_mapper, _user, _userQueries, _config);
            MethodResult<UserModelRequest> result = await handler.Handle(user, CancellationToken.None);
            result.ShouldBeOfType<MethodResult<UserModelRequest>>();
        }
        [Theory]
        [InlineData("", "test2")]
        [InlineData("test2", "")]
        public async Task RegisterFailMissingPassOrUserName(string username, string password)
        {
            MethodResult<UserModelRequest> methodResult = new();
            AuthUserCommand user = new()
            {
                Username = username,
                Password = password,
            };
            methodResult.StatusCode = StatusCodes.Status400BadRequest;
            methodResult.AddApiErrorMessage(
                string.IsNullOrEmpty(user.Username) ? nameof(EnumUserErrorCodes.USR05C) : nameof(EnumUserErrorCodes.USR06C),
                new[] { Helpers.GenerateErrorResult(nameof(user.Username), user.Username ?? "") }
            );
            AuthUserCommandHandler handler = new(_mapper, _user, _userQueries, _config);
            MethodResult<UserModelRequest> result = await handler.Handle(user, CancellationToken.None);
            bool resultMessageAndCode = CheckObjectEqual.ObjectAreEqual(result, methodResult);
            Assert.True(resultMessageAndCode);
        }
        [Theory]
        [InlineData("test12", "12345678Az*")]
        public async Task RegisterFailUsernameIsExist(string username, string password)
        {
            MethodResult<UserModelRequest> methodResult = new();
            AuthUserCommand user = new()
            {
                Username = username,
                Password = password,
            };
            methodResult.StatusCode = StatusCodes.Status400BadRequest;
            methodResult.AddApiErrorMessage(
                nameof(EnumUserErrorCodes.USR02C),
                new[] { Helpers.GenerateErrorResult(nameof(user.Username), user.Username ?? "") }
            );
            AuthUserCommandHandler handler = new(_mapper, _user, _userQueries, _config);
            MethodResult<UserModelRequest> result = await handler.Handle(user, CancellationToken.None);
            bool resultMessageAndCode = CheckObjectEqual.ObjectAreEqual(result, methodResult);
            Assert.True(resultMessageAndCode);
        }
        [Theory]
        [InlineData("testlocked", "1234567Az*99")]
        public async Task RegisterFailUsernameIsLocked(string username, string password)
        {
            MethodResult<UserModelRequest> methodResult = new();
            AuthUserCommand user = new()
            {
                Username = username,
                Password = password,
            };
            methodResult.StatusCode = StatusCodes.Status400BadRequest;
            methodResult.AddApiErrorMessage(
                nameof(EnumUserErrorCodes.USR28C),
                new[] { Helpers.GenerateErrorResult(nameof(user.Username), user.Username ?? "") }
            );
            AuthUserCommandHandler handler = new(_mapper, _user, _userQueries, _config);
            MethodResult<UserModelRequest> result = await handler.Handle(user, CancellationToken.None);
            bool resultMessageAndCode = CheckObjectEqual.ObjectAreEqual(result, methodResult);
            Assert.True(resultMessageAndCode);
        }
    }
}
