using Filmix.Models.AccountModels;
using Filmix.Services;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Filmix.Managers.Account
{
    public class AccountManager:IAccountManager
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private IEmailService EmailService { get; set; }   
        public AccountManager(UserManager<User> userManager, SignInManager<User> signInManager,IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            EmailService = emailService;
        }

        public async Task<RegisterResult> Register(RegisterViewModel model)
        {
            User user = new User { Email=model.Email,UserName=model.Email, Name = model.Name};
            RegisterResult userResult = new RegisterResult();

            if (_userManager.Users.Any(u => u.Name == user.Name))
            {
                userResult.Errors.Add("Пользователь с таким именем уже существует");
                userResult.Succeeded = false;
            }
            else if (_userManager.Users.Any(u => u.UserName == user.UserName))
            {
                userResult.Errors.Add("Пользователь с таким email уже существует");
                userResult.Succeeded = false;
            }
            else
                userResult.Succeeded = (await _userManager.CreateAsync(user, model.Password)).Succeeded;

            if (userResult.Succeeded)
            {
                userResult.Succeeded = true;
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                await EmailService.SendEmailAsync(user.Email, token);
            }
            
            return userResult;

        }

        public async Task<SignInResult> SignIn(LoginViewModel model)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.UserName == model.Login||u.Name==model.Login);
            var result= await _signInManager.PasswordSignInAsync(user.UserName, model.Password, false, false);


            return result;
        }

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
