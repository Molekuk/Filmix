using Filmix.Managers.Actors;
using Filmix.Managers.Films;
using Filmix.Managers.Genres;
using Filmix.Models.FilmModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Filmix.Controllers
{
    public class FilmController : Controller
    {
        private readonly IFilmManager _filmManager;
        private readonly IGenreManager _genreManager;
        private readonly IActorManager _actorManager;

        public FilmController(IFilmManager filmManager, IGenreManager genreManager, IActorManager actorManager)
        {
            _filmManager = filmManager;
            _genreManager = genreManager;
            _actorManager = actorManager;
        }

        public async Task<IActionResult> Index()
        {
            var films =  await _filmManager.GetFilmsViewModelAsync();
            return View(films);
        }
        
        public async Task<IActionResult> Film(int id)
        {
            var film = await _filmManager.FindAsync(id);
            return View(film);
        }

        public async Task<IActionResult> Actor(int id)
        {
            var actor = await _actorManager.FindAsync(id);
            return View(actor);
        }

        public async Task<IActionResult> Genre(int id)
        {
            var genre = await _genreManager.FindAsync(id);
            return View(genre);
        }
    }
}
