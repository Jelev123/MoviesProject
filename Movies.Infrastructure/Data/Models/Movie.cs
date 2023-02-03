namespace Movies.Infrastructure.Data.Models
{
    public class Movie
    {
        public int MovieId { get; set; }

        public string MovieName { get; set; }

        public int GenreId { get; set; }

        public string Director { get; set; }

        public string Actor { get; set; }

        public int YearId { get; set; }

        public Year Year { get; set; }

        public string CoverPhoto { get; set; }

        public string Country { get; set; }

        public Genre Genre { get; set; }

        public ICollection<Video> Videos { get; set; } = new HashSet<Video>();
    }
}
