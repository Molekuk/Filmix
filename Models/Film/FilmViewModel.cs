using System.Collections.Generic;

namespace Filmix.Models.FilmModels
{
    public class FilmViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int Year { get; set; }

        public decimal Rating { get; set; }

        public IEnumerable<string> GenreNames { get; set; }

        public string Image { get; set; }
    }
}
