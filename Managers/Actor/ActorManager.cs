using Filmix.Models.ActorModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filmix.Managers.Actors
{
    public class ActorManager:IActorManager
    {
        private ApplicationContext _context;
        public ActorManager(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Actor> FindAsync(int id)
        {
            return await _context.Actors.Include(a=>a.Films).ThenInclude(f=>f.Genres).FirstOrDefaultAsync(a=>a.Id==id);
        }

        

        public async Task AddAsync(Actor actor)
        {
            _context.Actors.Add(actor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var actor = await _context.Actors.FindAsync(id);
            _context.Actors.Remove(actor);
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<Actor>> GetActorsAsync()
        {
            return await _context.Actors.Include(a=>a.Films).ToListAsync();
        }

        public async Task UpdateAsync(Actor actor)
        {
            _context.Actors.Update(actor);
            await _context.SaveChangesAsync();
        }

        public async Task AddFilmsToActorAsync(int ActorId, IList<int> FilmIdList)
        {
            var films = await _context.Films.ToListAsync();
            var actor = await FindAsync(ActorId);

            if (actor.Films.Any())
                actor.Films.Clear();

            foreach (var id in FilmIdList)
            {
                actor.Films.Add(films.FirstOrDefault(f => f.Id == id));
            }

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<FilmInActorViewModel>> GetFilmsInActorAsync(Actor actor)
        {
            return await _context.Films.Select(f => new FilmInActorViewModel
            {
                FilmId=f.Id,
                FilmName=f.Name,
                IsInActor= actor.Films.Contains(f)
            }).ToListAsync();
        }

        public async Task<IEnumerable<ActorViewModel>> GetActorsViewModelAsync()
        {
            return await _context.Actors.Select(a => new ActorViewModel
            {
                Id= a.Id,
                Name= a.Name,
                Image= a.Image
            }).ToListAsync();
        }
    }
}
