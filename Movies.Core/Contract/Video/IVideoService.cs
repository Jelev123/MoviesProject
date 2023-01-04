namespace Movies.Core.Contracts.Video
{
    using Microsoft.AspNetCore.Http;
    using Movies.Core.ViewModels.Movie;
    using Movies.Core.ViewModels.Video;

    public interface IVideoService
    {
        Task CheckVideos(AddMovieViewModel model);

        //Task<string> UploadVideo(string folderPath, IFormFile file);
    }
}
