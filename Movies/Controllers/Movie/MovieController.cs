namespace Movies.Controllers.Movie
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Movies.Core.Contract.Genre;
    using Movies.Core.Contract.Movie;
    using Movies.Core.ViewModels.Genre;
    using Movies.Core.ViewModels.Movie;
    using Movies.Infrastructure.Data;

    public class MovieController : Controller
    {
        private readonly IMovieService movieService;
        private readonly IGenreService genreService;
        private readonly IWebHostEnvironment environment;
        private readonly ApplicationDbContext data;

        public MovieController(IMovieService movieService, IWebHostEnvironment environment, ApplicationDbContext data, IGenreService genreService)
        {
            this.movieService = movieService;
            this.environment = environment;
            this.data = data;
            this.genreService = genreService;
        }


        public IActionResult AddMovie()
        {
            var genres = this.genreService.AllGenres<AllGenreViewModel>();
            this.ViewData["genres"] = genres.Select(s => new AddMovieViewModel
            {
                GenreName = s.GenreName,
            }).ToList();

            return View();
        }

        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 214748364800)]
        public IActionResult AddMovie(AddMovieViewModel model)
        {
            this.movieService.AddMovie(model, $"{this.environment.WebRootPath}/Movies");
            return this.Redirect("/");
        }

        public IActionResult AllMovie()
        {
           var all = this.movieService.AllMovie();
            return this.View(all);
        }

        public IActionResult GetMovieById(int id)
        {
            var movie = this.movieService.GetMovieById(id);
            return this.View(movie);
        }

        public IActionResult AllMovieByGenre(int genreId)
        {
            var movie = this.movieService.AllMovieByGenre(genreId);
            return this.View(movie);
        }
    }
    
}

