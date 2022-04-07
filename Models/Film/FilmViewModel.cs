using System.Collections.Generic;

namespace Filmix.Models.FilmModels
{
    public class FilmViewModel
    {
        public string Name { get; set; }

        public string Year { get; set; }

        public decimal Rating { get; set; }

        public IEnumerable<string> Genres { get; set; }

        public string Image { get; set; }
    }
}
