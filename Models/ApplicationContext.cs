using Filmix.Models.AccountModels;
using Filmix.Models.FilmModels;
using Filmix.Models.GenreModels;
using Filmix.Models.ActorModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Filmix
{
    public class ApplicationContext:IdentityDbContext<User>
    {
        public DbSet<Film> Films { get; set; }
        public DbSet<Actor> Actors { get; set; }    
        public DbSet<Genre> Genres { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}
