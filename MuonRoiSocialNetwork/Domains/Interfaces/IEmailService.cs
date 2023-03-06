using MuonRoiSocialNetwork.Infrastructure.Extentions.Mail;

namespace MuonRoiSocialNetwork.Domains.Interfaces
{
    public interface IEmailService
    {
        Task SendTestEmail(UserEmailOptions userEmailOptions);

        Task SendEmailForEmailConfirmation(UserEmailOptions userEmailOptions);

        Task SendEmailForForgotPassword(UserEmailOptions userEmailOptions);
    }
}
