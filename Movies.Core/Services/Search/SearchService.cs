namespace Movies.Core.Services.Search
{
	using Microsoft.AspNetCore.Http;
    using Movies.Core.Contract.Movie;
    using Movies.Core.Contract.Search;
	using Movies.Core.ViewModels.Search;
	using Movies.Infrastructure.Data;
	using System.Collections.Generic;

	public class SearchService : ISearchService
	{

		private readonly ApplicationDbContext data;
		private readonly IMovieService movieService;

        public SearchService(ApplicationDbContext data, IMovieService movieService)
        {
            this.data = data;
            this.movieService = movieService;
        }

        public IEnumerable<SearchViewModel> SearchMovie(string genreName, string movieName)
		{
			var searchedMovive = this.data.Movies
				.Select(s => new SearchViewModel
				{
					MovieName = s.MovieName,
					Actor = s.Actor,
					Country = s.Country,
					Director = s.Director,
					CoverPhoto = s.CoverPhoto,
					Year = s.Year,
					GenreName = s.Genre.GenreName,
					MovieId = s.MovieId,
				})
				.Where(s => (s.GenreName.Contains(genreName)) || (s.MovieName.Contains(movieName)));

			return searchedMovive;
		}
	}
}
