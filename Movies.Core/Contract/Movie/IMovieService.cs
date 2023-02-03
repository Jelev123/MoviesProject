namespace Movies.Core.Contract.Movie
{
    using Movies.Core.ViewModels.Home;
    using Movies.Core.ViewModels.Movie;

    public interface IMovieService
    {
        Task AddMovie(AddMovieViewModel addMovie, string imagePath);

        IEnumerable<AddMovieViewModel> AllMovie(int page, int itemsPerPage = 100);

        AddMovieViewModel GetMovieById(int id);

        IEnumerable<IndexRandomViewModel> RandomMovies(int count);

        int GetCount();
    }
}
