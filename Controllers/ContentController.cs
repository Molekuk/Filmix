using Filmix.Managers.Films;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Filmix.Controllers
{
    public class ContentController : Controller
    {
        private readonly IFilmManager _filmManager;
        public ContentController(IFilmManager filmManager )
        {
            _filmManager = filmManager;
        }
        
        public async Task<IActionResult> Home()
        {
            var films =  await _filmManager.GetFilmsAsync();
            return View();
        }
    }
}
