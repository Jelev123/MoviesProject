namespace Movies.Controllers.Movie
{
    using Microsoft.AspNetCore.Mvc;
    using Movies.Core.Contract.Movie;
    using Movies.Core.ViewModels.Movie;
    using Movies.Infrastructure.Data;
   

    public class MovieController : Controller
    {
        private readonly IMovieService movieService;
        private readonly IWebHostEnvironment environment;
        private readonly ApplicationDbContext data;

        public MovieController(IMovieService movieService, IWebHostEnvironment environment, ApplicationDbContext data)
        {
            this.movieService = movieService;
            this.environment = environment;
            this.data = data;
        }


        public IActionResult AddMovie()
        {
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
    }
}
