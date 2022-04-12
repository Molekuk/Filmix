using Filmix.Models.FilmModels;
using Filmix.Models.GenreModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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
            return await _context.Genres.Include(g=>g.Films).FirstOrDefaultAsync(g=>g.Id==id);
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
            return await _context.Genres.Include(g=>g.Films).ToListAsync();
        }

        public async Task UpdateAsync(Genre genre)
        {
            _context.Genres.Update(genre);
            await _context.SaveChangesAsync();
        }

        public async Task AddFilmsToGenreAsync(int GenreId,IList<int> FilmIdList)
        {
            var films = await _context.Films.ToListAsync();
            var genre = await FindAsync(GenreId);

            if (genre.Films.Any())
                genre.Films.Clear();

            foreach (var id in FilmIdList)
            {
                genre.Films.Add(films.FirstOrDefault(f => f.Id == id));
            }

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<FilmInGenreViewModel>> GetFilmsInGenre(Genre genre)
        {
            return await _context.Films.Select(f => new FilmInGenreViewModel
            {
                FilmId = f.Id,
                FilmName = f.Name,
                IsInGenre = genre.Films.Contains(f)
            }).ToListAsync();
        }
      
    }
}
