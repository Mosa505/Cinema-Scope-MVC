using Microsoft.EntityFrameworkCore;

namespace Cinema_Scope.Models
{
    public class CinemaDbContext : DbContext
    {
        
        public CinemaDbContext(DbContextOptions<CinemaDbContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }





    }





}

