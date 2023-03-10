namespace Movies.Core.Contract.Search
{
    using Movies.Core.ViewModels.Search;

    public interface ISearchService
	{
        IEnumerable<SearchViewModel> SearchMovie(string genreName, string movieName, string year,  int page, int itemsPerPage = 6);
    }
}
