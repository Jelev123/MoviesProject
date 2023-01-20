namespace Movies.Core.ViewModels.Video
{
    using Microsoft.AspNetCore.Http;

    public class AddVideoViewModel
    {
        public List<IFormFile> VideoFiles { get; set; }

        public string  VideoName { get; set; }
    }
}
