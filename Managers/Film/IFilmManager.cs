using Filmix.Models.FilmModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Filmix.Managers.Films
{
    public interface IFilmManager
    {
        Task<IEnumerable<FilmViewModel>> GetFilmsAsync();

        Task<Film> FindFilmAsync(int id);
        Task AddFilmAsync(Film film);
    }
}
