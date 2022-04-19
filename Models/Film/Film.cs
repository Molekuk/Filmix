using Filmix.Models.ActorModels;
using Filmix.Models.GenreModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Filmix.Models.FilmModels
{
    public class Film
    {
        public int Id { get; set; }

        [Display(Name ="Название")]
        public string Name { get; set; }

        [Display(Name = "Название по-английски")]
        public string EngName { get; set; }

        [Display(Name = "Длительность")]
        public string Duration { get; set; }

        [Display(Name = "Название изображения для постера")]
        public string PosterImage { get; set; }

        [Display(Name = "Название изображения для плеера")]
        public string PlayerImage { get; set; }

        [Display(Name = "Название видеофайла")]
        public string Video { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Страна")]
        public string Country { get; set; }

        [Display(Name = "Режиссер")]
        public string Producer { get; set; }

        [Display(Name = "Кинокомпания")]
        public string Company { get; set; }

        [Display(Name = "Рейтинг")]
        public decimal Rating { get; set; }

        [Display(Name = "Год")]
        public string Year { get; set; }

        public  List<Genre> Genres { get; set; }

        public  List<Actor> Actors { get; set; }
    }
}
