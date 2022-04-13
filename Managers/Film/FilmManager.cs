using Filmix.Models.FilmModels;
using Filmix.Models.GenreModels;
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
            return await _context.Films.Include(f=>f.Actors).Include(f=>f.Genres).FirstOrDefaultAsync(f=>f.Id==id);
        }

        public async Task<IEnumerable<Film>> GetFilmsAsync()
        {
            return await _context.Films.Include(f=>f.Actors).Include(f=>f.Genres).ToListAsync();
        }

        public async Task<IEnumerable<FilmViewModel>> GetFilmsViewModelAsync()
        {
            return await _context.Films.Select(f=>new FilmViewModel
            {
                Id = f.Id,
                Name = f.Name,
                Year = f.Year,
                Rating = f.Rating,
                GenreNames = f.Genres.Select(g=>g.Name), 
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

        public async Task AddActorsToFilmAsync(int FilmId, IList<int> ActorIdList)
        {
            var actors = await _context.Actors.ToListAsync();
            var film = await FindAsync(FilmId);

            if(film.Actors.Any())
                film.Actors.Clear();

            foreach(var id in ActorIdList)
            {
                film.Actors.Add(actors.FirstOrDefault(a => a.Id == id));
            }

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ActorInFilmViewModel>> GetActorsInFilmAsync(Film film)
        {
            return await _context.Actors.Select(a => new ActorInFilmViewModel
            {
                ActorId = a.Id,
                ActorName = a.Name,
                IsInFilm = film.Actors.Contains(a)
            }).ToListAsync();
        }

        public async Task AddGenresToFilmAsync(int FilmId, IList<int> GenreIdList)
        {
            var genres = await _context.Genres.ToListAsync();
            var film = await FindAsync(FilmId);

            if (film.Genres.Any())
                film.Genres.Clear();

            foreach (var id in GenreIdList)
            {
                film.Genres.Add(genres.FirstOrDefault(g=>g.Id==id));
            }

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<GenreInFilmViewModel>> GetGenresInFilmAsync(Film film)
        {
            return await _context.Genres.Select(g => new GenreInFilmViewModel
            {
                GenreId = g.Id,
                GenreName = g.Name,
                IsInFilm = film.Genres.Contains(g)
            }).ToListAsync();
        }
    }
}
