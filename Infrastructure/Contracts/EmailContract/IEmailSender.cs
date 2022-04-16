using System.Threading.Tasks;

using Infrastructure.Models;

namespace Infrastructure.Contracts.EmailContract;

public interface IEmailSender
{
    Task<bool> SendEmailAsync(Email email);
}