using Filmix.Models.AccountModels;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Filmix.Managers.Account
{
    public class AccountManager:IAccountManager
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountManager(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> Register(RegisterViewModel model)
        {
            User user = new User { Email=model.Email,UserName=model.Email, Name = model.Name,DateBirth = model.DateBirth };
            var result = await _userManager.CreateAsync(user,model.Password);

            if(result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
            }
            else
            {
                
                foreach (var error in result.Errors)
                {
                    if (error.Code == "DuplicateUserName")
                        error.Description="Пользователь с таким Email уже существует";
                }
            }
            return result;
        }

        public async Task<SignInResult> SignIn(LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            return result;
        }

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
