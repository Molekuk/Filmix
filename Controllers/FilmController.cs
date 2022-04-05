using Filmix.Managers.Films;
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

        public FilmController(IFilmManager filmManager)
        {
            _filmManager = filmManager;
        }

        public async Task<IActionResult> Index()
        {
            var films =  await _filmManager.GetFilmsAsync();
            Console.WriteLine();
            return View();
        }

        public async Task<IActionResult> AddFilm()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddFilm(Film film)
        {
            await _filmManager.AddFilmAsync(film);
            return Content("Фильм успешно добавлен");
        }

    }
}
