using Serilog;
using System.IO;
using System.Threading.Tasks;
using YoussofPortfolio.API.Interfaces;
using YoussofPortfolio.API.Models;

namespace YoussofPortfolio.API.Services
{
    public class MailService : IMail
    {
        private readonly Mail.Core.Mail _mailSystem;

        public MailService()
        {
            _mailSystem = new Mail.Core.Mail("no-reply", "no-reply@youssofkhawaja.com", "email", "pwd", "smtp.gmail.com");
        }

        public async Task<bool> SendEmailAsync(Contact contact)
        {
            try
            {
                string mailContent = await File.ReadAllTextAsync("MailTemplates/Mail.html");
                mailContent = mailContent.Replace("[NAME]", contact.FirstName + " " + contact.LastName);

                string[] recipients = { };

                if (await _mailSystem.Send("Your mail received", mailContent, recipients, contact.Email))
                {
                    Log.Information("Mail sent successfully");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.Error($"Error sending email: {ex.Message}");
                throw;
            }

            return false;
        }

        public async Task<bool> SendEmailMeAsync(Contact contact)
        {
            try
            {
                string mailContent = await File.ReadAllTextAsync("MailTemplates/Mailme.html");
                mailContent = mailContent.Replace("[NAME]", contact.FirstName + " " + contact.LastName)
                                         .Replace("[Emailaddress]", contact.Email)
                                         .Replace("[Phonenumber]", contact.PhoneNumber)
                                         .Replace("[Message]", contact.Message);

                string[] recipients = { };

                if (await _mailSystem.Send("New contact message", mailContent, recipients, "snoopy.snowy123@gmail.com"))
                {
                    Log.Information("Mail sent successfully");
                    return true;
                }

            }
            catch (Exception ex)
            {
                Log.Error($"Error sending email: {ex.Message}");
                throw;
            }

            return false;
        }
    }
}
