namespace Movies.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Movies.Core.Contract.Genre;
    using Movies.Core.Contract.Movie;
    using Movies.Core.Contract.Search;
    using Movies.Core.Contract.Year;
    using Movies.Core.ViewModels;
    using Movies.Core.ViewModels.Genre;
    using Movies.Core.ViewModels.Home;
    using Movies.Core.ViewModels.Movie;
    using Movies.Core.ViewModels.Year;
    using Movies.Models;
    using System.Diagnostics;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMovieService movieService;
        private readonly IGenreService genreService;
        private readonly ISearchService searchService;
        private readonly IYearService yearService;

        public HomeController(ILogger<HomeController> logger, IMovieService movieService, IGenreService genreService, ISearchService searchService, IYearService yearService)
        {
            _logger = logger;
            this.movieService = movieService;
            this.genreService = genreService;
            this.searchService = searchService;
            this.yearService = yearService;
        }

        public IActionResult Index()
        {
            var genres = this.genreService.AllGenres<AllGenreViewModel>();
            var genreNames = genres.Select(s => s.GenreName);
            ViewBag.genreName = new SelectList(genreNames);

            var years = this.yearService.AllYears<AllYearViewModel>();
            var yearDate = years.Select(s => s.YearDate);
            ViewBag.yearDate = new SelectList(yearDate);

            const int Random = 10;
            var movies = new HomeViewModel
            {
                RandomMovies = this.movieService.RandomMovies(Random)
            };

            return this.View(movies);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}