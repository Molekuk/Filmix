using Filmix.Models.EmailModels;
using System.Threading.Tasks;

namespace Filmix.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string email,string token);

        EmailConfiguration GetConfiguration();
    }
}
