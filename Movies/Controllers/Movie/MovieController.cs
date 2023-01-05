namespace Movies.Controllers.Movie
{
    using Microsoft.AspNetCore.Mvc;
    using Movies.Core.Contract.Movie;
    using Movies.Core.ViewModels.Movie;

    public class MovieController : Controller
    {
        private readonly IMovieService movieService;
        private readonly IWebHostEnvironment environment;

        public MovieController(IMovieService movieService, IWebHostEnvironment environment)
        {
            this.movieService = movieService;
            this.environment = environment;
        }

        public IActionResult AddMovie()
        {
           
            return View();
        }

        [HttpPost]
        public IActionResult AddMovie(AddMovieViewModel model)
        {
            this.movieService.AddMovie(model, $"{this.environment.WebRootPath}/Movies");
            return View("/");
        }
    }
}
