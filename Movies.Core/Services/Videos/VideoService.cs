namespace Movies.Core.Service.Video
{
    using Movies.Core.Contracts.Video;
    using Movies.Core.ViewModels.Video;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Movies.Core.ViewModels.Movie;

    public class VideoService : IVideoService
    {
        private readonly IWebHostEnvironment environment;

        public VideoService(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }

        public async Task CheckVideos(AddMovieViewModel model)
        {
            if (model.VideoFiles != null)
            {
                string folder = "Movies/Videos/";

                model.Gallery = new List<VideoGalleryModel>();

                foreach (var file in model.VideoFiles)
                {
                    var gallery = new VideoGalleryModel()
                    {
                        VideoName = file.FileName,
                        MovieVideo = await UploadVideo(folder, file)
                    };
                    model.Gallery.Add(gallery);
                }
            }
        }

        public async Task<string> UploadVideo(string folderPath, IFormFile file)
        {
            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

            string serverFolder = Path.Combine(environment.WebRootPath, folderPath);

            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            return "/" + folderPath;
        }
    }
}
