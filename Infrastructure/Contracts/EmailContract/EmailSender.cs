using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

using Infrastructure.Models;

namespace Infrastructure.Contracts.EmailContract;

public class EmailSender : IEmailSender
{
    private readonly EmailSettings _emailSettings;

    public EmailSender(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task<bool> SendEmailAsync(Email email)
    {
        var client = new SendGridClient(_emailSettings.ApiKey);
        var to = new EmailAddress(email.To);
        var from = new EmailAddress
        {
            Email = _emailSettings.FromAddress,
            Name = _emailSettings.FromName
        };

        var message = MailHelper.CreateSingleEmail(from, to, email.Subject, email.Body, null);
        var response = await client.SendEmailAsync(message);

        return ((response.StatusCode == System.Net.HttpStatusCode.OK) || (response.StatusCode == System.Net.HttpStatusCode.Accepted));
    }
}