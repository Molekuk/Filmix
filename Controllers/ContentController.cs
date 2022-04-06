using Filmix.Managers.Films;
using Filmix.Managers.Genres;
using Filmix.Managers.Actors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Filmix.Models.ActorModels;

namespace Filmix.Controllers
{
    public class ContentController : Controller
    {
        private readonly IFilmManager _filmManager;
        private readonly IGenreManager _genreManager;
        private readonly IActorManager _actorManager;

        public ContentController(IFilmManager filmManager,IGenreManager genreManager ,IActorManager actorManager  )
        {
            _filmManager = filmManager;
            _genreManager = genreManager;
            _actorManager = actorManager;
        }
        
        public async Task<IActionResult> Home()
        {
            return View();
        }

        public IActionResult Films()
        {
            return View();
        }

        public async Task<IActionResult> Actors()
        { 
            var actors = await _actorManager.GetActorsAsync();
            return View(actors);
        }

        public async Task<IActionResult> EditActor(int id)
        {
            var actor = await _actorManager.FindAsync(id);
            return View(actor);
        }

        [HttpPost]
        public async Task<IActionResult> EditActor(Actor actor)
        {
            if (ModelState.IsValid)
            {
                await _actorManager.UpdateAsync(actor);
                return Content("Пользователь успешно изменен");
            }
            return View(actor);
        }

        public IActionResult Genres()
        {
            return View();
        }
    }
}
