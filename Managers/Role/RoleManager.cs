using Filmix.Models.AccountModels;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Filmix.Managers.Roles
{
    public class RoleManager:IRoleManager
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        public RoleManager(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task AddDefaultRolesAsync()
        {
            var user = new IdentityRole("user");
            var admin = new IdentityRole("admin");
            await _roleManager.CreateAsync(user);
            await _roleManager.CreateAsync(admin);
        }


        public async Task AddUserToRoleAsync(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                await _userManager.AddToRoleAsync(user, role);
            }
        }
    }
}
