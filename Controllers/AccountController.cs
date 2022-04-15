using Filmix.Managers.Account;
using Filmix.Models.AccountModels;
using Filmix.Models.EmailModels;
using Filmix.Services;
using MailKit.Net.Smtp;
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

        [HttpGet]
        [Route("/Register")]
        public async Task<IActionResult> Register()
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

                    return RedirectToAction("Index", "Filmix");
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


        [Route("/ConfirmEmail")]
        public IActionResult ConfirmEmail(string token)
        { 
            return Content($"Код был {token}");
        }


        [HttpGet]
        [Route("/Login")]
        public IActionResult Login()
        {
            return View(new LoginViewModel ());
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
                    ModelState.AddModelError("", "Неверный логин или пароль");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _accountManager.SignOut();
            return RedirectToAction("Index", "Filmix");
        }
    }
}
