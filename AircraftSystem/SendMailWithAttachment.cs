using System.Net.Mail;
using System.ComponentModel.DataAnnotations;
using AircraftSystem.Config;

namespace AircraftSystem
{
    public class SendMailWithAttachment
    {
        private string GetClientEmail()
        {
            do
            {
                Console.WriteLine("\nEnter email for report to be sent to:");
                string emailEntry = Console.ReadLine();
                var email = new EmailAddressAttribute();
                if (email.IsValid(emailEntry))
                {
                    return emailEntry;
                }
                else
                {
                    Console.WriteLine("\nIncorrect email!");
                }

            } while(true);
        }

        private string GetEmailSubject()
        {
            Console.WriteLine("\nEnter email subject:");
            return Console.ReadLine();
        }

        private string GetEmailBodyText()
        {
            Console.WriteLine("\nEnter email body text:");
            return Console.ReadLine();
        }

        private void SendMail(string recipientEmail, string subject, string bodyText, string reportAttachmentLocation)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(EmailConfig.smtpServer);

                mail.From = new MailAddress(EmailConfig.sendFromEmail);
                mail.To.Add($"{recipientEmail}");
                mail.Subject = $"{subject}";
                mail.Body = $"{bodyText}";

                Attachment attachment = new Attachment($"{reportAttachmentLocation}");
                mail.Attachments.Add(attachment);

                SmtpServer.Port = EmailConfig.serverPort;
                SmtpServer.Credentials = new System.Net.NetworkCredential(EmailConfig.userName, EmailConfig.password);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                Console.WriteLine("\nMail Sent! Double check in spam!\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return;
            }
        }

        public void ExecuteSendMailWithAttachment(string reportAttachmentLocation)
        {
            string clientEmail = GetClientEmail();
            string emailSubject = GetEmailSubject();
            string emailBodyText = GetEmailBodyText();
            SendMail(clientEmail, emailSubject, emailBodyText, reportAttachmentLocation);
        }
    }
}
