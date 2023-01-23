namespace Movies.Core.Service.Video
{
    using Movies.Core.Contracts.Video;
    using Movies.Core.ViewModels.Video;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Movies.Core.ViewModels.Movie;
    using System.Data.SqlClient;
    using System.Configuration;
    using Movies.Infrastructure.Data;

    public class VideoService : IVideoService
    {
        private readonly IWebHostEnvironment environment;
        private readonly ApplicationDbContext data;

        public VideoService(IWebHostEnvironment environment, ApplicationDbContext data)
        {
            this.environment = environment;
            this.data = data;
        }

        public async Task CheckVideos(AddMovieViewModel model)
        {
            if (model.Video != null)
            {
                model.Gallery = new List<VideoGalleryModel>();

                foreach (var video in model.Video)
                {
                    string folder = "Movies/Videos/";
                 
                    var movie = new VideoGalleryModel()
                    {
                        MovieVideo = await UploadVideo(folder, video.VideoFiles),
                        VideoName = video.VideoName,
                    };
                    model.Gallery.Add(movie);
                }
            }
        }
        public async Task<string> UploadVideo(string folderPath, IFormFile file)
        {
            folderPath +=  "_" + file.FileName;
            string serverFolder = Path.Combine(environment.WebRootPath, folderPath);
            file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            return "/" + folderPath;
        }
    }
}
