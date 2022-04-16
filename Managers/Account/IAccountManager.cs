using Filmix.Models.AccountModels;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Filmix.Managers.Account
{
    public interface IAccountManager
    {
        Task<ActionResult> Register(RegisterViewModel model);

        Task<ActionResult> ConfirmEmailAsync(string userId, string token);
        Task<ActionResult> SignIn(LoginViewModel model);

        Task<string> GetUserEmailAsync(string userId);
        Task SignOut();

    }
}
