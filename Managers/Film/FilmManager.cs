using Filmix.Models.FilmModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filmix.Managers.Films
{
    public class FilmManager:IFilmManager
    {
        private ApplicationContext _context;
        public FilmManager(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddFilmAsync(Film film)
        {
            _context.Add(film);
            await _context.SaveChangesAsync();
        }

        public async Task<Film> FindFilmAsync(int id)
        {
            return await _context.Films.FindAsync(id);
        }

        public async Task<IEnumerable<FilmViewModel>> GetFilmsAsync()
        {
            return await _context.Films.Select(f=>new FilmViewModel
            {
                Name = f.Name,
                Year = f.Year,
                Rating = f.Rating,
                Genre = f.Genre.Name,
                Image =f.Image
            }).ToListAsync();
        }
    }
}
