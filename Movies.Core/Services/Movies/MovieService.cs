namespace Movies.Core.Service.Movie
{
    using Movies.Core.Contract.Movie;
    using Movies.Core.ViewModels.Movie;
    using Movies.Infrastructure.Data;
    using System.Threading.Tasks;
    using Movies.Infrastructure.Data.Models;
    using Movies.Core.Contracts.Video;
    using System.Collections.Generic;
    using Movies.Core.ViewModels.Video;
    using Movies.Core.ViewModels.Home;

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
            var genre = this.data.Genres.FirstOrDefault(s => s.GenreName == addMovie.GenreName);
            videoService.CheckVideos(addMovie);
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
                    VideoName = file.VideoName,
                    VideoId = addMovie.VideoId,
                    MovieVideo = file.MovieVideo,
                });
            }
            data.Add(movie);
            data.SaveChanges();
            return Task.CompletedTask;
        }

        public IEnumerable<AddMovieViewModel> AllMovie(int page, int itemsPerPage = 6)
        {
            var all = this.data.Movies
                .Select(s => new AddMovieViewModel
                {
                    MovieId = s.MovieId,
                    MovieName = s.MovieName,
                    GenreId = s.GenreId,
                    GenreName = s.Genre.GenreName,
                    Actor = s.Actor,
                    Director = s.Director,
                    Country = s.Country,
                    CoverPhoto = s.CoverPhoto,
                    Year = s.Year,
                })
                .OrderBy(s => s.MovieName)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .ToList();

            return all;
        }

        public IEnumerable<AddMovieViewModel> SearchMovieByGenre(string genreName)
        {
            var all = this.data.Movies
                 .Where(s => s.Genre.GenreName == genreName)
                 .Select(s => new AddMovieViewModel
                 {
                     MovieId = s.MovieId,
                     MovieName = s.MovieName,
                     GenreId = s.GenreId,
                     GenreName = s.Genre.GenreName,
                     Actor = s.Actor,
                     Country = s.Country,
                     CoverPhoto = s.CoverPhoto,
                     Year = s.Year,
                     Director = s.Director,
                     Gallery = s.Videos
                     .Select(s => new VideoGalleryModel
                     {
                         VideoName = s.VideoName,
                         MovieVideo = s.MovieVideo,
                         MovieId = s.MovieId,
                         MovieSubs = s.MovieSubs,
                         CoverPhoto = s.Movie.CoverPhoto,
                     }).ToList()
                 });

            return all;
        }

        public AddMovieViewModel GetMovieById(int id)
        {
            var movie = this.data.Movies
                .Where(s => s.MovieId == id)
                .Select(s => new AddMovieViewModel
                {
                    MovieId = s.MovieId,
                    MovieName = s.MovieName,
                    GenreId = s.GenreId,
                    GenreName = s.Genre.GenreName,
                    Actor = s.Actor,
                    Country = s.Country,
                    CoverPhoto = s.CoverPhoto,
                    Year = s.Year,
                    Director = s.Director,
                    Gallery = s.Videos
                    .Select(s => new VideoGalleryModel
                    {
                        VideoName = s.VideoName,
                        MovieVideo = s.MovieVideo,
                        MovieId = s.MovieId,
                        MovieSubs = s.MovieSubs,
                        CoverPhoto = s.Movie.CoverPhoto,
                    }).ToList()
                }).FirstOrDefault();


            return movie;
        }

        public int GetCount()
        {
           return this.data.Movies.Count();
        }

        public IEnumerable<IndexRandomViewModel> RandomMovies(int count)
        {
            return this.data.Movies
                 .OrderBy(s => Guid.NewGuid())
                .Select(s => new IndexRandomViewModel
                {
                    MovieName = s.MovieName,
                    MovieId = s.MovieId,
                    GenreName = s.Genre.GenreName,
                    CoverPhoto = s.CoverPhoto,
                    Country = s.Country
                })
                .Take(count);
        }
    }
}
