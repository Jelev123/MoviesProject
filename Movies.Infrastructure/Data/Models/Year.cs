namespace Movies.Infrastructure.Data.Models
{
    public class Year
    {
        public int YearId { get; set; }

        public string YearDate { get; set; }

        public ICollection<Movie> Movies { get; set; } = new HashSet<Movie>();
    }
}
