namespace Movies.Controllers.Video
{
    using Microsoft.AspNetCore.Mvc;

    public class VideoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
