namespace Movies.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Movies.Core.Contract.Genre;
    using Movies.Core.Contract.Movie;
    using Movies.Core.Contract.Search;
    using Movies.Core.ViewModels;
    using Movies.Core.ViewModels.Genre;
    using Movies.Core.ViewModels.Movie;
    using Movies.Models;
    using System.Diagnostics;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMovieService movieService;
        private readonly IGenreService genreService;
        private readonly ISearchService searchService;

        public HomeController(ILogger<HomeController> logger, IMovieService movieService, IGenreService genreService, ISearchService searchService)
        {
            _logger = logger;
            this.movieService = movieService;
            this.genreService = genreService;
            this.searchService = searchService;
        }

        public IActionResult Index()
        {

            //var genres = this.genreService.AllGenres<AllGenreViewModel>();

            //var genreName = genres.Select(s => s.GenreName);

            //ViewBag.genreName = new SelectList(genreName);

            //this.ViewData["genres"] = genres.Select(s => new AddMovieViewModel
            //{
            //    GenreName = s.GenreName,
            //}).ToList();

            return this.View();
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