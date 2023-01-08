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
                model.Gallery = new List<VideoGalleryModel>();

                foreach (var video in model.VideoFiles)
                {
                    string folder = "Movies/Videos/";

                    foreach (var subs in model.MovieSubs)
                    {
                        var movie = new VideoGalleryModel()
                        {
                            VideoName = video.FileName,
                            MovieSubs = await UploadSubs(folder, subs),
                            MovieVideo = await UploadVideo(folder, video),
                        };
                        model.Gallery.Add(movie);
                    }
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

        public async Task<string> UploadSubs(string folderpath, IFormFile subs)
        {
            folderpath += "_" + subs.FileName;
            string serverFolder = Path.Combine(environment.WebRootPath, folderpath);
            subs.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            return "/" + folderpath;
        }
    }
}
