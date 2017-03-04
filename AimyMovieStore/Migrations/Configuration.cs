using AimyMovieStore.Models;
using System.Collections.Generic;

namespace AimyMovieStore.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<AimyMovieStore.Models.MovieContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AimyMovieStore.Models.MovieContext context)
        {
            var roles = new List<Movie>
            {
            new Movie{Name="Lion", Genre = "Drama", Price = 14.99},
            new Movie{Name="Moana", Genre = "Animation", Price = 19.99},
            new Movie{Name="Hidden Figures", Genre = "Drama", Price = 9.99}
            };
            roles.ForEach(s => context.Movies.AddOrUpdate(s));
            context.SaveChanges();
        }
    }
}
