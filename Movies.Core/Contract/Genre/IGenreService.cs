namespace Movies.Core.Contract.Genre
{
    using Movies.Core.ViewModels.Genre;

    public interface IGenreService
    {
        IEnumerable<AllGenreViewModel> AllGenres<T>();
    }
}
