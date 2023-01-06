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

                    var con = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    SqlConnection connection = new SqlConnection(con);
                    string query = "Insert into [dbo].[Videos] values(@MovieVideo,@MovieId)";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    command.Parameters.AddWithValue("@MovieVideo", gallery.MovieVideo);
                    command.Parameters.AddWithValue("@MovieId", gallery.MovieId);
                    command.ExecuteNonQuery();
                    connection.Close();
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
