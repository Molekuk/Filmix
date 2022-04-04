using Filmix.Models.FilmModels;
using System.Collections.Generic;

namespace Filmix.Models.GenreModels
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Image { get; set; }
        public string BackgroundImage { get; set; }

        public List<Film> Films { get; set; }
    }
}
