﻿using Filmix.Models.AccountModels;
using Filmix.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Filmix.Managers.Account
{
    public class AccountManager:IAccountManager
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private IEmailService EmailService { get; set; }   
        public AccountManager(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager,IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            EmailService = emailService;
        }

        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            User user = new User {Email=model.Email,UserName=model.Name};
            ActionResult userResult = new ActionResult();

            if ( (await _userManager.FindByEmailAsync(model.Email))!=null)
            {
                userResult.Errors.Add("Пользователь с таким email уже существует");
                userResult.Succeeded = false;
            }
            else if ((await _userManager.FindByNameAsync(model.Name)) != null)
            {
                userResult.Errors.Add("Пользователь с таким именем уже существует");
                userResult.Succeeded = false;
            }
            else
                userResult.Succeeded = (await _userManager.CreateAsync(user, model.Password)).Succeeded;

            if (userResult.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var code = HttpUtility.UrlEncode(token);
                await EmailService.SendConfirmEmailAsync(user.Email, code, user.Id);
            }

            return userResult;

        }

        public async Task<ActionResult> SignIn(LoginViewModel model)
        {
            ActionResult userResult = new ActionResult();
            var user = _userManager.Users.FirstOrDefault(u => u.UserName == model.Login||u.Email==model.Login);

            if (user != null)
            {
                if(!await _userManager.IsEmailConfirmedAsync(user))
                {
                    userResult.Errors.Add("Вы не подтвердили свой email");
                    return userResult;
                }
            }
            else
            {
                userResult.Errors.Add("Неправильный логин или пароль");
                return userResult;
            }
            userResult.Succeeded = (await _signInManager.PasswordSignInAsync(user.UserName, model.Password, false, false)).Succeeded;


            
            if(!userResult.Succeeded)
                userResult.Errors.Add("Неправильный логин или пароль");


            return userResult;
        }

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<ActionResult> ConfirmEmailAsync(string userId, string token)
        {
            ActionResult userResult = new ActionResult();
            if (userId == null || token == null)
            {
                userResult.Succeeded = false;
                return userResult;
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                userResult.Succeeded = false;
                return userResult;
            }

            userResult.Succeeded = (await _userManager.ConfirmEmailAsync(user, token)).Succeeded;
            
            return userResult;
        }

        public async Task<string> GetUserEmailAsync(string userId)
        {
            return (await _userManager.FindByIdAsync(userId)).Email;
        }
    }
}
