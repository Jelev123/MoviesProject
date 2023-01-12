namespace Movies.Infrastructure.Data.Models
{
    public class Video
    {
        public int VideoId { get; set; }

        public string MovieVideo { get; set; }

        public string? VideoName { get; set; }

        public string? MovieSubs { get; set; }

        public int MovieId { get; set; }

        public Movie Movie { get; set; }
       
    }
}
