namespace Movies.Infrastructure.Data.Models
{
    public class Genre
    {
        public int GenreId { get; set; }

        public string GenreName { get; set; }

        public ICollection<Movie> Movies { get; set; } = new HashSet<Movie>();
    }
}
