using System.Threading.Tasks;

namespace NorthWindApp.BLL.Infrastructure
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
