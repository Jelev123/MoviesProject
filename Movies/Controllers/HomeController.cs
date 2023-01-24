namespace Movies.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Movies.Core.Contract.Genre;
    using Movies.Core.Contract.Movie;
    using Movies.Core.ViewModels.Genre;
    using Movies.Core.ViewModels.Movie;
    using Movies.Models;
    using System.Diagnostics;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMovieService movieService;
        private readonly IGenreService genreService;

        public HomeController(ILogger<HomeController> logger, IMovieService movieService, IGenreService genreService)
        {
            _logger = logger;
            this.movieService = movieService;
            this.genreService = genreService;
        }

        public IActionResult Index()
        {
            var all = this.movieService.AllMovie();
            var genres = this.genreService.AllGenres<AllGenreViewModel>();
            this.ViewData["genres"] = genres.Select(s => new AddMovieViewModel
            {
                GenreName = s.GenreName,
            }).ToList();

            return this.View(all);
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