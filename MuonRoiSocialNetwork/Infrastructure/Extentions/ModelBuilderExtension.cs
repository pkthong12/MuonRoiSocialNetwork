
using BaseConfig.Extentions;
using ConnectVN.Social_Network.Categories;
using ConnectVN.Social_Network.Roles;
using ConnectVN.Social_Network.Storys;
using ConnectVN.Social_Network.Tags;
using ConnectVN.Social_Network.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MuonRoiSocialNetwork.Domains.DomainObjects.Groups;

namespace MuonRoiSocialNetwork.Infrastructure.Extentions
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Story>().HasData(
                new Story()
                {
                    Guid = Guid.NewGuid(),
                    Story_Title = "Ta Có Một Tòa Khí Vận Tế Đàn",
                    Story_Synopsis = "Bình An huyện nha dịch Trần Uyên xuyên qua mà đến, trong đầu cất giấu một tòa khí vận tế đàn .  Chỉ cần hiến tế khí vận, liền có thể thu được thiên cơ chỉ dẫn, thần thông, công pháp, tà thuật, thiên tài địa bảo ...  Mọi loại đều là hạ phẩm, chỉ có tập võ cao!  Đại Tấn những năm cuối, Tây vực Phật môn truyền đạo Trung Nguyên, Nam Cương yêu tộc nhìn chằm chằm .  Bắc man thiết kỵ 300 ngàn, uy áp biên cảnh .  Đạo môn chân nhân, Kiếm Tông kiếm tiên, ma đạo cự phách, giang hồ danh túc ... Thiên hạ đem loạn!  Đây là xấu nhất thời đại, cũng là tốt nhất thời đại ...  Ta gọi Trần Uyên, đến từ vực sâu!  Sát phạt quả đoán .  Chúc bạn có những giây phút vui vẻ khi đọc truyện Ta Có Một Tòa Khí Vận Tế Đàn!",
                    Img_Url = "aacd50da-4e0d-47e4-939c-a4ace0f707ea.image/jpeg*Stories/ConnectVn_ta-co-mot-toa-khi-van-te-dan-cbde5bc2e8.jpg",
                    IsShow = true,
                    TotalView = 0,
                    TotalFavorite = 0,
                    Rating = 0,
                    Slug = "ta-co-mot-toa-khi-van-te-dan",
                    CreatedDateTS = new DateTime(2023, 01, 01).GetTimeStamp(),
                    CreatedUserId = 1,
                    IsDeleted = false,
                    CreatedUserName = "muonroi",
                    CategoryId = 1
                },
                new Story()
                {
                    Guid = Guid.NewGuid(),
                    Story_Title = "Nhân Sinh Tùy Tiện Bắt Đầu Từ Tuổi Ba Mươi (Bản Dịch)",
                    Story_Synopsis = "Người khác xuyên việt trẻ thêm vài tuổi, Trần Tự xuyên việt thành ông chú 30.  Tưởng đâu đã có mái ấm êm đềm, ai ngờ xuyên đến lại đúng dịp ly hôn  Nếu như đã không có gì để mà lo lắng, vậy thì sống cho thật thoải mái đi.",
                    Img_Url = "079dec71-43fd-4701-8450-a1ad1e6c39ff.image/jpeg*Stories/ConnectVn_nhan-sinh-tuy-tien-bat-dau-tu-tuoi-ba-muoi-ban-dic-7a54bfe686.jpg",
                    IsShow = true,
                    TotalView = 0,
                    TotalFavorite = 0,
                    Rating = 0,
                    Slug = "nhan-sinh-tuy-tien-bat-dau-tu-tuoi-ba-muoi",
                    CreatedDateTS = new DateTime(2023, 01, 01).GetTimeStamp(),
                    CreatedUserId = 1,
                    IsDeleted = false,
                    CreatedUserName = "muonroi",
                    CategoryId = 1,
                });
            var hashPassword = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(new AppUser()
            {
                //[Guid("E0223A03-2945-49DB-976E-736433465B7F")]
                Id = new Guid("E0223A03-2945-49DB-976E-736433465B7F"),
                UserName = "muonroi",
                Name = "Phi Le",
                Surname = "Anh",
                Email = "leanhphi1706@gmail.com",
                PasswordHash = hashPassword.HashPassword(null, "0967442142Az*"),
                PhoneNumber = "093.310.5367",
                Status = EnumAccountStatus.Active

            });
            modelBuilder.Entity<Category>().HasData(new Category()
            {
                Id = 1,
                NameCategory = "Tiên hiệp",
                IsActive = true,
                CreatedDateTS = new DateTime(2023, 01, 01).GetTimeStamp(),
                CreatedUserName = "muonroi",
                IsDeleted = false,
                CreatedUserId = 1
            },
            new Category()
            {
                Id = 2,
                NameCategory = "Huyền huyễn",
                IsActive = true,
                CreatedDateTS = new DateTime(2023, 01, 01).GetTimeStamp(),
                CreatedUserName = "muonroi",
                IsDeleted = false,
                CreatedUserId = 1
            });
            modelBuilder.Entity<Tag>().HasData(new Tag()
            {
                Id = 1,
                TagName = "Đã hoàn thành",
                Details = "Truyện đã hoàn thành xong",
                CreatedDateTS = new DateTime(2023, 01, 01).GetTimeStamp(),
                CreatedUserName = "muonroi",
                IsDeleted = false,
                CreatedUserId = 1
            },
            new Tag()
            {
                Id = 2,
                TagName = "Chưa hoàn thành",
                Details = "Truyện chưa hoàn thành xong",
                CreatedDateTS = new DateTime(2023, 01, 01).GetTimeStamp(),
                CreatedUserName = "muonroi",
                IsDeleted = false,
                CreatedUserId = 1
            });
            modelBuilder.Entity<AppRole>().HasData(new AppRole()
            {
                //[Guid("72377426-B057-46CA-98FF-1CA9D33EA0C1")]
                Id = new Guid("72377426-B057-46CA-98FF-1CA9D33EA0C1"),
                Name = "Administratior",
                Description = "Người quản lý cao nhất"
            });
            modelBuilder.Entity<GroupUserMember>().HasData(new GroupUserMember()
            {
                Id = 1,
                AppRoleKey = new Guid("72377426-B057-46CA-98FF-1CA9D33EA0C1"),
                AppUserKey = new Guid("E0223A03-2945-49DB-976E-736433465B7F"),
                CreatedDateTS = new DateTime(2023, 01, 01).GetTimeStamp(),
                CreatedUserName = "muonroi",
                CreatedUserId = 1
            });
        }
    }
}
