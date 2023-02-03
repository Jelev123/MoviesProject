namespace Movies.Controllers.Movie
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Movies.Core.Contract.Genre;
    using Movies.Core.Contract.Movie;
    using Movies.Core.Contract.Search;
    using Movies.Core.Contract.Year;
    using Movies.Core.ViewModels.Genre;
    using Movies.Core.ViewModels.Movie;
    using Movies.Core.ViewModels.Year;
    using Movies.Infrastructure.Data;

    public class MovieController : Controller
    {
        private readonly IMovieService movieService;
        private readonly IGenreService genreService;
        private readonly ISearchService searchService;
        private readonly IYearService yearService;
        private readonly IWebHostEnvironment environment;
        private readonly ApplicationDbContext data;

        public MovieController(IMovieService movieService, IWebHostEnvironment environment, ApplicationDbContext data, IGenreService genreService, ISearchService searchService, IYearService yearService)
        {
            this.movieService = movieService;
            this.environment = environment;
            this.data = data;
            this.genreService = genreService;
            this.searchService = searchService;
            this.yearService = yearService;
        }


        public IActionResult AddMovie()
        {
            var genres = this.genreService.AllGenres<AllGenreViewModel>();
            this.ViewData["genres"] = genres.Select(s => new AddMovieViewModel
            {
                GenreName = s.GenreName,
            }).ToList();

            var year = this.yearService.AllYears<AllYearViewModel>();

            var years = ViewData["years"] = year.Select(s => new AddMovieViewModel
            {
                YearDate = s.YearDate,
            });

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
            var genreNames = genres.Select(s => s.GenreName);
            ViewBag.genreName = new SelectList(genreNames);

            var years = this.yearService.AllYears<AllYearViewModel>();
            var yearDates = years.Select(s => s.YearDate);
            ViewBag.yearDate = new SelectList(yearDates);

            if (id <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 10;

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

        public IActionResult SearchMovie(string genreName, string movieName, string yearDate, int id = 1)
        {

            if (genreName == null && movieName == null && yearDate == null)
            {
                return this.RedirectToAction("Index", "Home");
            }
            else if (true)
            {

            }

            if (movieName != null)
            {
                genreName = null;

            }


            if (id <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 10;

            var searchedMovies = new MovieListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                MoviesCount = this.movieService.GetCount(),
                SearchedMovies = this.searchService.SearchMovie(genreName, movieName, yearDate, id, ItemsPerPage)
            };


            var genres = this.genreService.AllGenres<AllGenreViewModel>();
            var years = this.yearService.AllYears<AllYearViewModel>();

            var genreNames = genres.Select(s => s.GenreName);
            ViewBag.genreName = new SelectList(genreNames);

            var yearDates = years.Select(s => s.YearDate);
            ViewBag.yearDate = new SelectList(yearDates);

            this.ViewData["searchMovie"] = movieName;
          
            return this.View(searchedMovies);
        }
    }
    
}

