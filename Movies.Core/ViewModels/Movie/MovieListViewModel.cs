namespace Movies.Core.ViewModels.Movie
{
    using Movies.Core.ViewModels.Search;
    using System.Collections.Generic;

    public class MovieListViewModel : PagingViewModel
    {
        public IEnumerable<AddMovieViewModel> Movies { get; set; }

        public IEnumerable<SearchViewModel> SearchedMovies { get; set; }
    }
}
