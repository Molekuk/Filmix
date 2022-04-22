using Filmix.Models.FilmModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Filmix.Managers.Films
{
    public interface IFilmManager
    {
        Task<IEnumerable<FilmViewModel>> GetFilmsViewModelAsync(int minYear, int maxYear, int minRating, int maxRating);

        Task<IEnumerable<Film>> GetFilmsAsync();

        Task<Film> FindAsync(int id);

        Task AddAsync(Film film);

        Task UpdateAsync(Film film);

        Task DeleteAsync(int id);

        Task AddActorsToFilmAsync(int FilmId, IList<int> ActorIdList);

        Task<IEnumerable<ActorInFilmViewModel>> GetActorsInFilmAsync(Film film);

        Task AddGenresToFilmAsync(int FilmId, IList<int> GenreIdList);

        public Task<IEnumerable<GenreInFilmViewModel>> GetGenresInFilmAsync(Film film);
    }
}
