using Filmix.Models.FilmModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Filmix.Models.ActorModels
{
    public class Actor
    {
        public int Id { get; set; }

        [Display(Name ="Имя")]
        public string Name { get; set; }

        public string Slug { get; set; }

        [Display(Name = "Изображение")]
        public string Image { get; set; }

        [Display(Name = "Дата рождения")]
        public string DateBirth { get; set; }

        [Display(Name = "Место рождения")]
        public string PlaceBirth { get; set; }

        [Display(Name = "Рост")]
        public byte Height { get; set; }

        [Display(Name = "Жанры")]
        public string Genres { get; set; }

        [Display(Name = "Количество фильмов")]
        public int FilmCount { get; set; }

        public  List<Film> Films { get; set; } 
    
    }
}
