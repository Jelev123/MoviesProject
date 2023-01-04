namespace Movies.Core.Service.Movie
{
    using Movies.Core.Contract.Movie;
    using Movies.Core.ViewModels.Movie;
    using Movies.Infrastructure.Data;
    using System.Threading.Tasks;
    using Movies.Infrastructure.Data.Models;
    using Movies.Core.Contracts.Video;

    public class MovieService : IMovieService
    {
        private readonly ApplicationDbContext data;
        private readonly IVideoService videoService;

        public MovieService(ApplicationDbContext data, IVideoService videoService)
        {
            this.data = data;
            this.videoService = videoService;
        }

        public Task AddMovie(AddMovieViewModel addMovie, string imagePath)
        {
            videoService.CheckVideos(addMovie);
            var genre = this.data.Genres.FirstOrDefault(s => s.GenreName == addMovie.GenreName);
            var movie = new Movie
            {
                MovieName = addMovie.MovieName,
                GenreId = genre.GenreId,
                Actor = addMovie.Actor,
                Country = addMovie.Country,
                Director = addMovie.Director,
                Year = addMovie.Year,
                CoverPhoto = addMovie.CoverPhoto,
            };


            movie.Videos = new List<Video>();

            foreach (var file in addMovie.Gallery)
            {
                movie.Videos.Add(new Video()
                {
                    MovieVideo = file.MovieVideo,
                });
            }

            data.Add(movie);
            data.SaveChanges();
            return Task.CompletedTask;
        }
    }
}
