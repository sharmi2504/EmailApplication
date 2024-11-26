using System.Net.Mail;
using System.Net;

namespace EmailApplication.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmailWithAttachment(string toEmail, string subject, string body, string pdfPath)
        {
            var fromEmail = _configuration["SmtpSettings:Email"];
            var fromPassword = _configuration["SmtpSettings:Password"];

            using (var smtpClient = new SmtpClient("smtp.gmail.com", 587))
            {
                smtpClient.Credentials = new NetworkCredential(fromEmail, fromPassword);
                smtpClient.EnableSsl = true;

                var mailMessage = new MailMessage(fromEmail, toEmail, subject, body)
                {
                    IsBodyHtml = true
                };

                if (File.Exists(pdfPath)) 
                {
                    var attachment = new Attachment(pdfPath); 
                    mailMessage.Attachments.Add(attachment); 
                }
                else
                {
                    throw new FileNotFoundException("PDF file not found.");
                }

                // Log or debug attachment details
                System.Diagnostics.Debug.WriteLine($"Attachment added: {pdfPath}");
                System.Diagnostics.Debug.WriteLine($"Attachment size: {new FileInfo(pdfPath).Length} bytes");

                smtpClient.Send(mailMessage);
            }
        }
    }
}
