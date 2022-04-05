﻿using Filmix.Models.FilmModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Filmix.Managers.Films
{
    public interface IFilmManager
    {
        Task<IEnumerable<FilmViewModel>> GetFilmsViewModelAsync();

        Task<IEnumerable<Film>> GetFilmsAsync();


        Task<Film> FindAsync(int id);

        Task AddAsync(Film film);

        Task UpdateAsync(Film film);

        Task DeleteAsync(int id);
    }
}
