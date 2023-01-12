namespace Movies.Core.ViewModels.Movie
{
    using Microsoft.AspNetCore.Http;

    public class AddSubtitles
    {
        public List<IFormFile> Subtitles { get; set; }
    }
}
