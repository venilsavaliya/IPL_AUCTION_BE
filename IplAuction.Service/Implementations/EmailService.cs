using IplAuction.Service.Interface;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace IplAuction.Service.Implementations;

public class EmailService(IConfiguration configuration) : IEmailService
{
    private readonly IConfiguration _configuration = configuration;

    public bool SendEmail(string toEmail, string subject, string htmlMessage)
    {
        var emailSettings = _configuration.GetSection("EmailSettings");
        // ServicePointManager.CheckCertificateRevocationList = false;

        var emailMessage = new MimeMessage();
        emailMessage.From.Add(MailboxAddress.Parse(emailSettings["FromEmail"]));
        emailMessage.To.Add(MailboxAddress.Parse(toEmail));
        emailMessage.Subject = subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };

        using var client = new SmtpClient();
        client.Connect(emailSettings["SmtpServer"], int.Parse(emailSettings["Port"]!), false);
        client.Authenticate(emailSettings["SenderEmail"], emailSettings["SenderPassword"]);
        client.Send(emailMessage);
        client.Disconnect(true);

        return true;
    }
}
