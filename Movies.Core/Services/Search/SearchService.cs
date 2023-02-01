namespace Movies.Core.Services.Search
{
	using Microsoft.AspNetCore.Http;
	using Movies.Core.Contract.Search;
	using Movies.Core.ViewModels.Search;
	using Movies.Infrastructure.Data;
	using System.Collections.Generic;

	public class SearchService : ISearchService
	{

		private readonly ApplicationDbContext data;

		public SearchService(ApplicationDbContext data)
		{
			this.data = data;
		}

		public IEnumerable<SearchViewModel> SearchMovie(string genreName)
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
				.Where(s => s.GenreName.Contains(genreName))
				.ToList();

			return searchedMovive;
		}
	}
}
