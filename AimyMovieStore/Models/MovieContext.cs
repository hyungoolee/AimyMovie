using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace AimyMovieStore.Models
{
    public class MovieContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
