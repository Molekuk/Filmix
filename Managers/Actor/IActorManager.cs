using Filmix.Models.ActorModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Filmix.Managers.Actors
{
    public interface IActorManager
    {
        Task<IEnumerable<Actor>> GetActorsAsync();
        Task<IEnumerable<ActorViewModel>> GetActorsViewModelAsync();
        Task<Actor> FindAsync(int id);
        Task AddAsync(Actor actor);
        Task UpdateAsync(Actor actor);
        Task DeleteAsync(int id);
        Task AddFilmsToActorAsync(int ActorId, IList<int> FilmIdList);
        Task<IEnumerable<FilmInActorViewModel>> GetFilmsInActorAsync(Actor actor);
    }
}
