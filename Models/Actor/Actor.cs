using Filmix.Models.FilmModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Filmix.Models.ActorModels
{
    public class Actor
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Slug { get; set; }

        public string Image { get; set; }

        public string DateBirth { get; set; }

        public string PlaceBirth { get; set; }

        public byte Height { get; set; }

        public string Genres { get; set; }

        public int FilmCount { get; set; }

        public  List<Film> Films { get; set; } 
    
    }
}
