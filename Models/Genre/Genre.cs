using Filmix.Models.FilmModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Filmix.Models.GenreModels
{
    public class Genre
    {
        public int Id { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }
        public string Slug { get; set; }

        [Display(Name = "Изображение")]
        public string Image { get; set; }

        [Display(Name = "Задний фон")]
        public string BackgroundImage { get; set; }

        public  List<Film> Films { get; set; }
    }
}
