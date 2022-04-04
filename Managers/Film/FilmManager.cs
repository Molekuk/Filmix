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

        public async Task<IList<Film>> GetFilmsAsync()
        {
           return await _context.Films.ToListAsync();
        }
    }
}
