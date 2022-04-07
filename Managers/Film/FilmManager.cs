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
            return await _context.Films.Include(f=>f.Actors).FirstOrDefaultAsync(f=>f.Id==id);
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

        public async Task AddActorsToFilmAsync(int FilmId, IList<int> ActorIdList)
        {
            var film = await FindAsync(FilmId);
            var actors = await _context.Actors.ToListAsync();

            if(film.Actors.Any())
                film.Actors.Clear();

            foreach(var id in ActorIdList)
            {
                film.Actors.Add(actors[id-1]);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ChangeActorViewModel>> GetChangeFilmsViewModelAsync(Film film)
        {
            return await _context.Actors.Select(a => new ChangeActorViewModel
            {
                ActorId = a.Id,
                ActorName = a.Name,
                IsInFilm = film.Actors.Contains(a)
            }).ToListAsync();
        }
    }
}
