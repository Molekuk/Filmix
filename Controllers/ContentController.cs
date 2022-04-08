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
                return RedirectToAction("Success", "Content", new { message = $"Информация об актере {actor.Name} успешно изменена!" });
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
            return RedirectToAction("Success", "Content", new { message = $"Актер {actor.Name} успешно добавлен!" });
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
                return RedirectToAction("Success", "Content", new { message = $"Информация о фильме {film.Name} успешно изменена!" });
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
            return RedirectToAction("Success", "Content", new { message = $"Фильм {film.Name} успешно добавлен!" });
        }

        public async Task<IActionResult> ChangeActorsInFilm(int? filmId)
        {
            if (filmId is null)
                return Content("Не указан Id фильма");

            var film = await _filmManager.FindAsync(filmId.Value);
            var actors = await _filmManager.GetChangeFilmsViewModelAsync(film);

            ViewBag.Id = film.Id;
            ViewBag.Name = film.Name;

            return View(actors);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeActorsInFilm(int filmId, IList<int> containsActorId)
        {
            await _filmManager.AddActorsToFilmAsync(filmId, containsActorId);
            string filmName = (await _filmManager.FindAsync(filmId)).Name;
            return RedirectToAction("Success", "Content", new { message = $"Актеры фильма {filmName} успешно изменены!" });
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
                return RedirectToAction("Success", "Content", new { message = $"Информация о жанре {genre.Name} успешно изменена!" });
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
            return RedirectToAction("Success", "Content", new { message = $"Жанр {genre.Name} успешно добавлен!" });
        }

        public async Task<IActionResult> ChangeFilmsInGenre(int? GenreId)
        {
            if (GenreId is null)
                return Content("Не указан Id жанра");

            var genre = await _genreManager.FindAsync(GenreId.Value);
            var films = await _genreManager.GetChangeFilmsViewModelAsync(genre);

            ViewBag.Id=genre.Id;
            ViewBag.Name=genre.Name;

            return View(films);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeFilmsInGenre(int genreId,IList<int> containsFilmId)
        {
            await _genreManager.AddFilmToGenreAsync(genreId,containsFilmId);
            string genreName = (await _genreManager.FindAsync(genreId)).Name;
            return RedirectToAction("Success", "Content", new { message = $"Фильмы жанра {genreName} успешно изменены!" });
        }
        #endregion

        public IActionResult Success(string message)
        {
            ViewBag.Msg=message;
            return View();
        }

        
    }


}
