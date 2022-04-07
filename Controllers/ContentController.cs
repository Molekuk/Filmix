using Filmix.Managers.Films;
using Filmix.Managers.Genres;
using Filmix.Managers.Actors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Filmix.Models.ActorModels;
using System.Linq;
using System.Collections.Generic;
using Filmix.Models.FilmModels;
using Filmix.Models.GenreModels;

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
        
        public IActionResult Home()
        {
            return View();
        }


        #region Актеры
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
                return Content("Информация об актере успешно изменена!");
            }
            return View(actor);
        }
        public IActionResult AddActor()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddActor(Actor actor)
        {
            await _actorManager.AddAsync(actor);
            return Content("Актер успешно добавлен!");
        }
        #endregion

        #region Фильмы
        public async Task<IActionResult> Films()
        {
            var films = await _filmManager.GetFilmsAsync();
            return View(films);
        }

        public async Task<IActionResult> EditFilm(int id)
        {
            var film = await _filmManager.FindAsync(id);
            return View(film);
        }

        [HttpPost]
        public async Task<IActionResult> EditFilm(Film film)
        {
            if (ModelState.IsValid)
            {
                await _filmManager.UpdateAsync(film);
                return Content("Информация о фильме успешно изменена!");
            }
            return View();
        }

        public IActionResult AddFilm()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddFilm(Film film)
        {
            await _filmManager.AddAsync(film);
            return Content("Фильм успешно добавлен!");
        }

        #endregion

        #region Жанры
        public async Task<IActionResult> Genres()
        {
            var genres = await _genreManager.GetGenresAsync();
            return View(genres);
        }

        public async Task<IActionResult> EditGenre(int id)
        {
            var genre = await _genreManager.FindAsync(id);
            return View(genre);
        }

        [HttpPost]
        public async Task<IActionResult> EditGenre(Genre genre)
        {
            if (ModelState.IsValid)
            {
                await _genreManager.UpdateAsync(genre);
                return Content("Информация о жанре успешно изменена!");
            }
            return View();
        }

        public IActionResult AddGenre()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddGenre(Genre genre)
        {
            await _genreManager.AddAsync(genre);
            return Content("Жанр успешно добавлен!");
        }

        public async Task<IActionResult> ChangeFilmsInGenre(int? GenreId)
        {
            if (GenreId is null)
                return Content("Не указать Id жанра");

            var genre = await _genreManager.FindAsync(GenreId.Value);
            var films = await _genreManager.GetFilmsAddViewModelAsync(genre);

            ViewBag.Id=genre.Id;
            ViewBag.Name=genre.Name;

            return View(films);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeFilmsInGenre(int GenreId,IList<int> ContainsFilmId)
        {
            await _genreManager.AddFilmToGenreAsync(GenreId,ContainsFilmId);
            return Content("Фильмы жанра успешно изменены");
        }
        #endregion
    }


}
