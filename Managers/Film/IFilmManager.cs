using Filmix.Models.FilmModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Filmix.Managers.Films
{
    public interface IFilmManager
    {
        Task<IList<Film>> GetFilmsAsync();

        Task AddFilmAsync(Film film);
    }
}
