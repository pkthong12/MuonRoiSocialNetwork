using Moq;
using MuonRoi.Social_Network.Users;
using MuonRoiSocialNetwork.Domains.Interfaces;

namespace TestProject1
{
    public static class Class1
    {
        public static Mock<IUserRepository> Test_Register_Controller()
        {
            int s = 0;
            var user = new AppUser
            {
                Name = "test",
                Surname = "test2",
                Email = "leanhphi1706@gmail.com",
                PhoneNumber = "1234567890",
                BirthDate = new DateTime(2002, 06, 17),
                UserName = "test1",
                PasswordHash = "1234567Az*99",
            };
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(r => r.CreateNewUserAsync(user)).ReturnsAsync(s);
            return mockRepo;
        }
    }
}
