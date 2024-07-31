using Serilog;
using System.Net.Mail;
using System.Text;

namespace YoussofPortfolio.API.Mail.Core;

public class Mail
{
    private readonly string Name;
    private readonly string SenderEmail;
    private readonly string Email;
    private readonly string Password;
    private readonly string Host;
    private readonly int Port;
    public Mail(string name, string senderemail, string email, string password, string host, int port = 587)
    {
        Name = name;
        SenderEmail = senderemail;
        Email = email;
        Password = password;
        Host = host;
        Port = port;
    }
    public async Task<bool> Send(string subject, string content, string[] ccemails, params string[] toemails)
    {
            SmtpClient smtpClient = new(Host, Port)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(Email, Password),
                DeliveryMethod = SmtpDeliveryMethod.Network 
            };
            MailMessage mail = new()
            {
                IsBodyHtml = true,
                Subject = subject,
                Body = content,
                BodyEncoding = Encoding.UTF8,
                SubjectEncoding = Encoding.UTF8,
                From = new MailAddress(SenderEmail, Name)
            };
            foreach (string email in toemails)
            {
                mail.To.Add(new MailAddress(email));
            }
            if (ccemails is not null)
            {
                foreach (string cc in ccemails)
                {
                    mail.CC.Add(new MailAddress(cc));
                }
            }

            mail.Subject = subject;
            mail.Body = content;

        try
        {
            await smtpClient.SendMailAsync(mail);
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
            return false;
        }

        return true;
    }
}