using Filmix.Models.ActorModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
            return await _context.Actors.Include(a=>a.Films).FirstOrDefaultAsync(a=>a.Id==id);
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
    }
}
