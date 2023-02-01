namespace Movies.Controllers.Movie
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Movies.Core.Contract.Genre;
    using Movies.Core.Contract.Movie;
    using Movies.Core.Contract.Search;
    using Movies.Core.ViewModels;
    using Movies.Core.ViewModels.Genre;
    using Movies.Core.ViewModels.Movie;
    using Movies.Infrastructure.Data;

    public class MovieController : Controller
    {
        private readonly IMovieService movieService;
        private readonly IGenreService genreService;
        private readonly ISearchService searchService;
        private readonly IWebHostEnvironment environment;
        private readonly ApplicationDbContext data;

        public MovieController(IMovieService movieService, IWebHostEnvironment environment, ApplicationDbContext data, IGenreService genreService, ISearchService searchService)
        {
            this.movieService = movieService;
            this.environment = environment;
            this.data = data;
            this.genreService = genreService;
            this.searchService = searchService;
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

        public IActionResult AllMovie(int id = 1)
        {

            var genres = this.genreService.AllGenres<AllGenreViewModel>();

            ViewBag.genreName = new SelectList(genres);
            const int ItemsPerPage = 1;

            var all = new MovieListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                MoviesCount = this.movieService.GetCount(),
                Movies = this.movieService.AllMovie(id,ItemsPerPage),
            };
           
            return this.View(all);
        }

        public IActionResult GetMovieById(int id)
        {
            var movie = this.movieService.GetMovieById(id);
            return this.View(movie);
        }

        public IActionResult SearchMovieByGenre(string genreName)
        {

            var genres = this.genreService.AllGenres<AllGenreViewModel>();

            ViewBag.genreName = new SelectList(genres);
            var genreNames = genres.Select(s => s.GenreName).ToString();

            var searchedMovie = this.searchService.SearchMovie(genreName);

            if (searchedMovie == null)
            {
                return this.View(genreName);
            }
            return this.View(searchedMovie);
        }
    }
    
}

