using AutoMapper;
using Moq;
using MuonRoiSocialNetwork.Application.Commands.Users;
using MuonRoiSocialNetwork.Domains.Interfaces;
using MuonRoiSocialNetwork.Infrastructure.Map.Users;
using Xunit;
using Microsoft.Extensions.Configuration;
using Shouldly;
using BaseConfig.MethodResult;
using MuonRoiSocialNetwork.Common.Models.Users;
using Xunit.Sdk;

namespace MuonRoiSocialNetwork.Test
{
    public class Class2
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUserRepository> _user;
        private IConfiguration _config;
        private IEmailService _mail;
        public Class2(IConfiguration config, IEmailService emailService)
        {
            _user = Class1.Test_Register_Controller();
            var mapperctg = new MapperConfiguration(c =>
            {
                c.AddProfile<UserProfile>();
            });
            _mapper = mapperctg.CreateMapper();
            _config = config;
            _mail = emailService;
        }
        [Fact]
        public async Task Register()
        {
            UserModel s = new();
            var handler = new CreateUserCommandHandler(_mapper, _user.Object, _config, _mail);
            var result = await handler.Handle(new CreateUserCommand(), CancellationToken.None);
            result.ShouldBeOfType<MethodResult<UserModel>>();
            //result.Result.ShouldBe(s);
        }
    }
}
