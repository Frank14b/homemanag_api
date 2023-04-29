using System.Text;
using API.DTOs.Emails;
using API.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using RazorEngineCore;

namespace API.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendEmailAsync(EmailRequestDto mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail);
            email.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail));
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            if (mailRequest.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in mailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }

            builder.HtmlBody = GetEmailTemplate("users", mailRequest);

            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.Auto);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public string GetEmailTemplate(string emailTemplate, EmailRequestDto mailRequest)
        {
            string mailTemplate = LoadTemplate(emailTemplate);

            IRazorEngine razorEngine = new RazorEngine();
            IRazorEngineCompiledTemplate modifiedMailTemplate = razorEngine.Compile(mailTemplate);
            
            return modifiedMailTemplate.Run(mailRequest);
        }

        public string LoadTemplate(string emailTemplate)
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string templateDir = Path.Combine("", "/Users/kamgafrank/Documents/projects/homemanag/API/Views/Emails");
            string templatePath = Path.Combine(templateDir, $"{emailTemplate}.cshtml");

            using FileStream fileStream = new FileStream(templatePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using StreamReader streamReader = new StreamReader(fileStream, Encoding.Default);
            
            string mailTemplate = streamReader.ReadToEnd();
            streamReader.Close();

            return mailTemplate;
        }
    }
}