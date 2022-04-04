using Filmix.Models.AccountModels;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Filmix.Managers.Account
{
    public interface IAccountManager
    {
        public Task<IdentityResult> Register(RegisterViewModel model);
        public Task<SignInResult> SignIn(LoginViewModel model);
        public Task SignOut();

    }
}
