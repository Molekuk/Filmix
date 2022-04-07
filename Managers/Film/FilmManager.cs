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


        public async Task<Film> FindAsync(int id)
        {
            return await _context.Films.FindAsync(id);
        }

        public async Task<IEnumerable<Film>> GetFilmsAsync()
        {
            return await _context.Films.Include(f=>f.Actors).Include(f=>f.Genres).ToListAsync();
        }

        public async Task<IEnumerable<FilmViewModel>> GetFilmsViewModelAsync()
        {
            return await _context.Films.Select(f=>new FilmViewModel
            {
                Name = f.Name,
                Year = f.Year,
                Rating = f.Rating,
                Genres = f.Genres.Select(g=>g.Name), 
                Image =f.Image
            }).ToListAsync();
        }

        public async Task AddAsync(Film film)
        {
            _context.Films.Add(film);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Film film)
        {
            _context.Films.Update(film);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var film = await FindAsync(id);
            _context.Films.Remove(film);
            await _context.SaveChangesAsync();
        }



    }
}
