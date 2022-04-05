using Filmix.Models.AccountModels;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Filmix.Managers.Account
{
    public interface IAccountManager
    {
        Task<IdentityResult> Register(RegisterViewModel model);
        Task<SignInResult> SignIn(LoginViewModel model);
        Task SignOut();

    }
}
