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
                return RedirectToAction("Actors", "Content");
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
            return RedirectToAction("Actors", "Content");
        }

        public async Task<IActionResult> ChangeFilmsInActor(int? actorId)
        {
            if (actorId is null)
                return Content("Не указан Id актера");

            var actor = await _actorManager.FindAsync(actorId.Value);
            var films = await _actorManager.GetFilmsInActorAsync(actor);

            ViewBag.Id = actor.Id;
            ViewBag.Name = actor.Name;

            return View(films);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeFilmsInActor(int actorId, IList<int> containsFilmId)
        {
            await _actorManager.AddFilmsToActorAsync(actorId, containsFilmId);
            return RedirectToAction("Actors", "Content");
        }

        public async Task<IActionResult> DeleteActor(int actorId)
        {
            await _actorManager.DeleteAsync(actorId);
            return RedirectToAction("Actors", "Content");
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
                return RedirectToAction("Films", "Content");
            }
            return View(film);
        }

        public IActionResult AddFilm()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddFilm(Film film)
        {
            await _filmManager.AddAsync(film);
            return RedirectToAction("Films", "Content");
        }

        public async Task<IActionResult> ChangeActorsInFilm(int? filmId)
        {
            if (filmId is null)
                return Content("Не указан Id фильма");

            var film = await _filmManager.FindAsync(filmId.Value);
            var actors = await _filmManager.GetActorsInFilmAsync(film);

            ViewBag.Id = film.Id;
            ViewBag.Name = film.Name;

            return View(actors);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeActorsInFilm(int filmId, IList<int> containsActorId)
        {
            await _filmManager.AddActorsToFilmAsync(filmId, containsActorId);
            return RedirectToAction("Films", "Content");
        }

        public async Task<IActionResult> ChangeGenresInFilm(int? filmId)
        {
            if (filmId is null)
                return Content("Не указан Id фильма");

            var film = await _filmManager.FindAsync(filmId.Value);
            var genres = await _filmManager.GetGenresInFilmAsync(film);

            ViewBag.Id = film.Id;
            ViewBag.Name = film.Name;

            return View(genres);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeGenresInFilm(int filmId, IList<int> containsGenreId)
        {
            await _filmManager.AddGenresToFilmAsync(filmId, containsGenreId);
            return RedirectToAction("Films", "Content");
        }

        public async Task<IActionResult> DeleteFilm(int filmId)
        {
            await _filmManager.DeleteAsync(filmId);
            return RedirectToAction("Films", "Content");
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
                return RedirectToAction("Genres", "Content");
            }
            return View(genre);
        }

        public IActionResult AddGenre()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddGenre(Genre genre)
        {
            await _genreManager.AddAsync(genre);
            return RedirectToAction("Genres", "Content");
        }

        public async Task<IActionResult> ChangeFilmsInGenre(int? genreId)
        {
            if (genreId is null)
                return Content("Не указан Id жанра");

            var genre = await _genreManager.FindAsync(genreId.Value);
            var films = await _genreManager.GetFilmsInGenre(genre);

            ViewBag.Id=genre.Id;
            ViewBag.Name=genre.Name;

            return View(films);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeFilmsInGenre(int genreId,IList<int> containsFilmId)
        {
            await _genreManager.AddFilmsToGenreAsync(genreId,containsFilmId);
            return RedirectToAction("Genres", "Content");
        }

        public async Task<IActionResult> DeleteGenre(int genreId)
        {
            await _genreManager.DeleteAsync(genreId);
            return RedirectToAction("Genres", "Content");
        }
        #endregion




    }


}
