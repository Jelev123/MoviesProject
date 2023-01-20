namespace Movies.Core.ViewModels.Movie
{
    using Microsoft.AspNetCore.Http;
    using Movies.Core.ViewModels.Video;

    public class AddMovieViewModel
    {
        public int MovieId { get; set; }

        public string MovieName { get; set; }

        public string VideoName { get; set; }

        public string GenreName { get; set; }

        public int GenreId { get; set; }

        public string Director { get; set; }

        public string Actor { get; set; }

        public string Year { get; set; }

        public string CoverPhoto { get; set; }

        public string Country { get; set; }

        public int VideoId { get; set; }


        public List<AddVideoViewModel> Video { get; set; }

        public List<IFormFile> VideoFiles { get; set; }

        public List<IFormFile> MovieSubs { get; set; }

        public List<VideoGalleryModel> Gallery { get; set; }
    }
}
