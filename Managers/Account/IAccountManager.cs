using Filmix.Models.AccountModels;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Filmix.Managers.Account
{
    public interface IAccountManager
    {
        Task<SignResult> RegisterAsync(RegisterViewModel model);
        Task<SignResult> ConfirmEmailAsync(string userId, string token);
        Task<SignResult> SignInAsync(LoginViewModel model);
        Task<string> GetUserEmailAsync(string userId);
        Task SignOutAsync();

    }
}
