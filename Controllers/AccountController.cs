using Filmix.Managers.Account;
using Filmix.Managers.Roles;
using Filmix.Models.AccountModels;
using Filmix.Models.EmailModels;
using Filmix.Services;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Threading.Tasks;

namespace Filmix.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountManager _accountManager;
        public AccountController(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        public IActionResult AccessDenied()
        {
            return RedirectToAction("Index","Filmix");
        }

        [HttpGet]
        [Route("/Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("/Register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountManager.Register(model);
                if (result.Succeeded)
                {

                    return View("ConfirmEmail");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(model);
        }




        [HttpGet]
        [Route("/Login")]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [Route("/ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromServices] IEmailService emailService,string userId, string token)
        {
            var result =  await _accountManager.ConfirmEmailAsync(userId, token);
            if (result.Succeeded)
            {
                var email = await _accountManager.GetUserEmailAsync(userId);
                await emailService.SendSuccessRegisterEmailAsync(email);
                return RedirectToAction("Login", "Account");
            }
            else
                return Content("Ошибка");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                var result = await _accountManager.SignIn(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Filmix");
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _accountManager.SignOut();
            return RedirectToAction("Index", "Filmix");
        }
    }
}
