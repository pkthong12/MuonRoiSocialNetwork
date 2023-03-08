using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using MuonRoi.Social_Network.Users;
using MuonRoiSocialNetwork.Common.Settings.Appsettings;
using MuonRoiSocialNetwork.Domains.Interfaces;
using MuonRoiSocialNetwork.Infrastructure;
using MuonRoiSocialNetwork.Infrastructure.Map.Users;
using MuonRoiSocialNetwork.Infrastructure.Repositories;

namespace MuonRoiSocialNetwork.Test
{
    public class MockDataBase
    {
        public IConfiguration _configBase;
        public Mock<IEmailService> _emailServiceBase;
        public UserRepository _userRepoBase;
        public MuonRoiSocialNetworkDbContext _userdbContext;
        public MapperConfiguration _mapperConfiguration;
        public IMapper _maperBase;
        public MockDataBase()
        {
            ServiceProvider serviceProvider = new ServiceCollection()
                               .AddEntityFrameworkInMemoryDatabase()
                               .BuildServiceProvider();
            DbContextOptionsBuilder<MuonRoiSocialNetworkDbContext> builder = new DbContextOptionsBuilder<MuonRoiSocialNetworkDbContext>()
                        .UseInMemoryDatabase(Guid.NewGuid().ToString())
                        .UseInternalServiceProvider(serviceProvider);
            _userdbContext = new MuonRoiSocialNetworkDbContext(builder.Options);
            _userRepoBase = new UserRepository(_userdbContext);
            _configBase = new ConfigurationBuilder().AddJsonFile($"{NameAppSetting.APPSETTINGS}.json", optional: false).Build();
            _emailServiceBase = new Mock<IEmailService>();
            _mapperConfiguration = new MapperConfiguration(c =>
            {
                c.AddProfile<UserProfile>();
            });
            _maperBase = _mapperConfiguration.CreateMapper();

            #region InitData User
            AppUser user1 = new()
            {
                Id = new Guid("6A2A1E72-7C06-47AC-861C-75046C75A588"),
                Name = "test",
                Surname = "test2",
                Email = "leanhphi1706@gmail.com",
                PhoneNumber = "1234567890",
                BirthDate = new DateTime(2002, 06, 17),
                UserName = "test1",
                PasswordHash = "1234567Az*99",
            };
            AppUser user2 = new()
            {
                Id = new Guid("00F13A88-54B8-4A07-9AFB-636C2C93C200"),
                Name = "test2",
                Surname = "test3",
                Email = "leanhphi1707@gmail.com",
                PhoneNumber = "1234567890",
                BirthDate = new DateTime(2002, 06, 18),
                UserName = "test2",
                PasswordHash = "1234567Az*99",
            };
            if (!_userdbContext.Users.Any(t => t.Id.Equals(new Guid("6A2A1E72-7C06-47AC-861C-75046C75A588"))))
                _userdbContext.Users.Add(user1);
            if (!_userdbContext.Users.Any(t => t.Id.Equals(new Guid("00F13A88-54B8-4A07-9AFB-636C2C93C200"))))
                _userdbContext.Users.Add(user2);
            _userdbContext.SaveChanges();
            #endregion
        }
    }
}
