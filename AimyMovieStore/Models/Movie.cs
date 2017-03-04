using System.ComponentModel.DataAnnotations;

namespace AimyMovieStore.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Genre { get; set; }
        public double Price { get; set; }
    }
}
