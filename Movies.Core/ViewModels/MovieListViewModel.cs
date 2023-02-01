namespace Movies.Core.ViewModels
{
    using Movies.Core.ViewModels.Movie;
    using System.Collections.Generic;

    public class MovieListViewModel : PagingViewModel
    {
        public IEnumerable<AddMovieViewModel> Movies { get; set; }
    }
}
