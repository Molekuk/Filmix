using System.Threading.Tasks;

namespace Filmix.Managers.Roles
{
    public interface IRoleManager
    {
        Task AddDefaultRolesAsync();
        Task AddUserToRoleAsync(string userId,string role);
    }
}
