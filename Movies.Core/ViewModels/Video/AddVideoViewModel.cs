namespace Movies.Core.ViewModels.Video
{
    using Microsoft.AspNetCore.Http;

    public class AddVideoViewModel
    {
        public IFormFileCollection VideoFiles { get; set; }

        public List<VideoGalleryModel> Gallery { get; set; }
    }
}
