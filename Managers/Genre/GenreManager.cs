using Filmix.Models.GenreModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Filmix.Managers.Genres
{
    public class GenreManager:IGenreManager
    {
        private ApplicationContext _context;
        public GenreManager(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Genre> FindAsync(int id)
        {
            return await _context.Genres.FindAsync(id);
        }
        public async Task AddAsync(Genre genre)
        {
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<Genre>> GetGenresAsync()
        {
            return await _context.Genres.ToListAsync();
        }

        public async Task UpdateAsync(Genre genre)
        {
            _context.Genres.Update(genre);
            await _context.SaveChangesAsync();
        }
    }
}
