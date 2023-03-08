using AutoMapper;
using Moq;
using MuonRoiSocialNetwork.Domains.Interfaces;
using Microsoft.Extensions.Configuration;
using MuonRoiSocialNetwork.Test;
using MuonRoiSocialNetwork.Infrastructure.Map.Users;
using MuonRoiSocialNetwork.Common.Models.Users;
using MuonRoiSocialNetwork.Application.Commands.Users;
using Shouldly;
using BaseConfig.MethodResult;
using MuonRoi.Social_Network.Users;
using MuonRoiSocialNetwork.Infrastructure.Repositories;
using BaseConfig.EntityObject.Entity;
using Microsoft.AspNetCore.Http;

namespace TestProject1
{
    public class CreateUserCommandTest
    {
        MockDataBase baseData = new();
        private readonly IMapper _mapper;
        public UserRepository _user;
        private readonly IConfiguration _config;
        private readonly Mock<IEmailService> _mail;
        public CreateUserCommandTest()
        {
            _user = baseData._userRepo;
            var mapperctg = new MapperConfiguration(c =>
            {
                c.AddProfile<UserProfile>();
            });
            _mapper = mapperctg.CreateMapper();
            _config = baseData._config;
            _mail = baseData._emailService;
        }
        [Fact]
        public async Task RegisterSuccess()
        {
            var user = new CreateUserCommand
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
            var handler = new CreateUserCommandHandler(_mapper, _user, _config, _mail.Object);
            var result = await handler.Handle(user, CancellationToken.None);
            result.ShouldBeOfType<MethodResult<UserModel>>();
        }
        [Fact]
        public async Task RegisterFail_NotValidRequest()
        {
            var user = new CreateUserCommand
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
            MethodResult<UserModel> methodResult = new()
            {
                StatusCode = StatusCodes.Status400BadRequest
            };
            methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USR13C),
                        new[] { Helpers.GenerateErrorResult(nameof(user.UserName), user.UserName ?? "") }
                    );
            var handler = new CreateUserCommandHandler(_mapper, _user, _config, _mail.Object);
            var result = await handler.Handle(user, CancellationToken.None);
            result.ShouldBeOfType<MethodResult<UserModel>>();
        }
        [Fact]
        public async Task RegisterFail_UserIsExist()
        {
            var user = new CreateUserCommand
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
            var handler = new CreateUserCommandHandler(_mapper, _user, _config, _mail.Object);
            var result = await handler.Handle(user, CancellationToken.None);
            var resultMessageAndCode = result.ErrorMessages.ElementAt(0).ErrorMessage.Equals(methodResult.ErrorMessages.ElementAt(0).ErrorMessage) && result.ErrorMessages.ElementAt(0).ErrorCode.Equals(methodResult.ErrorMessages.ElementAt(0).ErrorCode);
            Assert.True(resultMessageAndCode);
        }
    }
}