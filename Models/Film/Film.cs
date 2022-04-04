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
        public string Name { get; set; }
        public string Slug { get; set; }
        [MaxLength(8)]
        public string Duration { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public string Producer { get; set; }
        public string Company { get; set; }
        [Range(0,10)]
        public decimal Rating { get; set; }
        [MaxLength(4)]
        public string Year { get; set; }

        public int GenreId { get; set; }
        [ForeignKey("GenreId")]
        public virtual Genre Genre { get; set; }

        public  List<Actor> Actors { get; set; }
    }
}
