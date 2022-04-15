using Filmix.Models.AccountModels;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Filmix.Managers.Account
{
    public interface IAccountManager
    {
        Task<RegisterResult> Register(RegisterViewModel model);
        Task<SignInResult> SignIn(LoginViewModel model);
        Task SignOut();

    }
}
