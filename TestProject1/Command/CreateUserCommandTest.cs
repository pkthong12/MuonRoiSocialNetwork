using AutoMapper;
using Moq;
using MuonRoiSocialNetwork.Domains.Interfaces;
using Microsoft.Extensions.Configuration;
using MuonRoiSocialNetwork.Test;
using MuonRoiSocialNetwork.Common.Models.Users;
using MuonRoiSocialNetwork.Application.Commands.Users;
using Shouldly;
using BaseConfig.MethodResult;
using MuonRoi.Social_Network.Users;
using MuonRoiSocialNetwork.Infrastructure.Repositories;
using BaseConfig.EntityObject.Entity;
using Microsoft.AspNetCore.Http;
using BaseConfig.Extentions;

namespace TestProject1
{
    public class CreateUserCommandTest
    {
        private readonly MockDataBase _baseData = new();
        private readonly IMapper _mapper;
        public readonly UserRepository _user;
        private readonly IConfiguration _config;
        private readonly Mock<IEmailService> _mail;
        public CreateUserCommandTest()
        {
            _user = _baseData._userRepoBase;
            _mapper = _baseData._maperBase;
            _config = _baseData._configBase;
            _mail = _baseData._emailServiceBase;
        }
        [Fact]
        public async Task RegisterSuccess()
        {
            CreateUserCommand user = new()
            {
                Name = "test",
                Surname = "test2",
                Email = "leanhphi1706@gmail.com",
                PhoneNumber = "1234567890",
                BirthDate = DateTime.Now,
                UserName = "test11",
                PasswordHash = "1234567Az*99",
                Address = "string",
                Gender = MuonRoi.Social_Network.User.EnumGender.Male,
                LastLogin = new DateTime(2023, 01, 01),
                Avatar = "string",
                Status = EnumAccountStatus.UnConfirm,
                Note = "string"
            };
            CreateUserCommandHandler handler = new(_mapper, _user, _config, _mail.Object);
            MethodResult<UserModel> result = await handler.Handle(user, CancellationToken.None);
            result.ShouldBeOfType<MethodResult<UserModel>>();
        }
        [Fact]
        public async Task RegisterFail_NotValidRequest()
        {
            CreateUserCommand user = new()
            {
                Name = "test",
                Surname = "test2",
                Email = "leanhphi@1706@gmail.com",
                PhoneNumber = "1234567890",
                BirthDate = DateTime.Now,
                UserName = "test11",
                PasswordHash = "12345",
                Address = "string",
                Gender = MuonRoi.Social_Network.User.EnumGender.Male,
                LastLogin = new DateTime(2023, 01, 01),
                Avatar = "string",
                Status = EnumAccountStatus.UnConfirm,
                Note = "string"
            };
            AppUser newUser = _mapper.Map<AppUser>(user);
            MethodResult<UserModel> methodResult = new()
            {
                StatusCode = StatusCodes.Status400BadRequest
            };
            newUser.IsValid();
            methodResult.AddResultFromErrorList(newUser.ErrorMessages);
            CreateUserCommandHandler handler = new(_mapper, _user, _config, _mail.Object);
            MethodResult<UserModel> result = await handler.Handle(user, CancellationToken.None);
            bool resultMessageAndCode = CheckObjectEqual.ObjectAreEqual(result, methodResult);
            Assert.True(resultMessageAndCode);
        }
        [Fact]
        public async Task RegisterFail_UserIsExist()
        {
            CreateUserCommand user = new()
            {
                Name = "test",
                Surname = "test2",
                Email = "leanhphi1706@gmail.com",
                PhoneNumber = "1234567890",
                BirthDate = DateTime.Now,
                UserName = "test1",
                PasswordHash = "1234567Az*99",
                Address = "string",
                Gender = MuonRoi.Social_Network.User.EnumGender.Male,
                LastLogin = new DateTime(2023, 01, 01),
                Avatar = "string",
                Status = EnumAccountStatus.UnConfirm,
                Note = "string"
            };
            MethodResult<UserModel> methodResult = new()
            {
                StatusCode = StatusCodes.Status400BadRequest
            };
            methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USR13C),
                        new[] { Helpers.GenerateErrorResult(nameof(user.UserName), user.UserName ?? "") }
                    );
            CreateUserCommandHandler handler = new(_mapper, _user, _config, _mail.Object);
            MethodResult<UserModel> result = await handler.Handle(user, CancellationToken.None);
            bool resultMessageAndCode = CheckObjectEqual.ObjectAreEqual(result, methodResult);
            Assert.True(resultMessageAndCode);
        }
        [Fact]
        public async Task RegisterFail_DbContextIsNull()
        {
            CreateUserCommand user = new()
            {
                Name = "test",
                Surname = "test2",
                Email = "leanhphi1706@gmail.com",
                PhoneNumber = "1234567890",
                BirthDate = DateTime.Now,
                UserName = "test1",
                PasswordHash = "1234567Az*99",
                Address = "string",
                Gender = MuonRoi.Social_Network.User.EnumGender.Male,
                LastLogin = new DateTime(2023, 01, 01),
                Avatar = "string",
                Status = EnumAccountStatus.UnConfirm,
                Note = "string"
            };
            MethodResult<UserModel> methodResult = new()
            {
                StatusCode = StatusCodes.Status400BadRequest
            };
            var _users = new UserRepository(null);
            CreateUserCommandHandler handler = new(_mapper, _users, _config, _mail.Object);
            MethodResult<UserModel> result = await handler.Handle(user, CancellationToken.None);
            bool resultMessageAndCode = CheckObjectEqual.ObjectAreEqual(result, methodResult);
            Assert.False(resultMessageAndCode);
        }
    }
}