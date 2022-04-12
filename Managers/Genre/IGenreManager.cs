using Filmix.Models.FilmModels;
using Filmix.Models.GenreModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Filmix.Managers.Genres
{
    public interface IGenreManager
    {
        Task<IEnumerable<Genre>> GetGenresAsync();


        Task<Genre> FindAsync(int id);

        Task AddAsync(Genre genre);

        Task UpdateAsync(Genre genre);
        Task DeleteAsync(int id);

        Task AddFilmsToGenreAsync(int GenreId, IList<int> FilmIdList);

        public Task<IEnumerable<FilmInGenreViewModel>> GetFilmsInGenre(Genre genre);
    }
}
