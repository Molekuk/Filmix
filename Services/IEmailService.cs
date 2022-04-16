using Filmix.Models.EmailModels;
using System.Threading.Tasks;

namespace Filmix.Services
{
    public interface IEmailService
    {
        Task<bool> SendConfirmEmailAsync(string email,string token, string userId);
        Task SendSuccessRegisterEmailAsync(string email);

        EmailConfiguration GetConfiguration();
    }
}
