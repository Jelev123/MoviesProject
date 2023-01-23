namespace Movies.Core.ViewModels.Video
{
    using Microsoft.AspNetCore.Http;

    public class AddVideoViewModel
    {
        public IFormFile VideoFiles { get; set; }

        public string  VideoName { get; set; }
    }
}
