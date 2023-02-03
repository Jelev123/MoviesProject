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

        public IEnumerable<SearchViewModel> SearchMovie(string genreName, string movieName, string year,  int page, int itemsPerPage = 6)
		{


			var searchedMovive = this.data.Movies
				.Select(s => new SearchViewModel
				{
					MovieName = s.MovieName,
					Actor = s.Actor,
					Country = s.Country,
					Director = s.Director,
					CoverPhoto = s.CoverPhoto,
					Year = s.Year.YearDate,
					GenreName = s.Genre.GenreName,
					MovieId = s.MovieId,
				})
				.Where(s => (s.MovieName.Contains(movieName)));


			//if (movieName != null)
   //         {
			//	var searchedMoviveByName = this.data.Movies
			//	.Select(s => new SearchViewModel
			//	{
			//		MovieName = s.MovieName,
			//		Actor = s.Actor,
			//		Country = s.Country,
			//		Director = s.Director,
			//		CoverPhoto = s.CoverPhoto,
			//		Year = s.Year.YearDate,
			//		GenreName = s.Genre.GenreName,
			//		MovieId = s.MovieId,
			//	})
			//	.Where(s => (s.MovieName.Contains(movieName)));
			//	return searchedMoviveByName;
			//}


            if (genreName != null)
            {
				var searchedMoviveByGenre = this.data.Movies
				.Select(s => new SearchViewModel
				{
					MovieName = s.MovieName,
					Actor = s.Actor,
					Country = s.Country,
					Director = s.Director,
					CoverPhoto = s.CoverPhoto,
					Year = s.Year.YearDate,
					GenreName = s.Genre.GenreName,
					MovieId = s.MovieId,
				})
				.Where(s => (s.GenreName.Contains(genreName)));

				return searchedMoviveByGenre;
			}

			if (year != null)
			{
				var searchedMoviveByYear = this.data.Movies
				.Select(s => new SearchViewModel
				{
					MovieName = s.MovieName,
					Actor = s.Actor,
					Country = s.Country,
					Director = s.Director,
					CoverPhoto = s.CoverPhoto,
					Year = s.Year.YearDate,
					GenreName = s.Genre.GenreName,
					MovieId = s.MovieId,
				})
				.Where(s => (s.Year.Contains(year)));

				return searchedMoviveByYear;
			}


			if (genreName != null && year != null)
            {
				var searchedMoviveByGenreAndYear = this.data.Movies
				.Select(s => new SearchViewModel
				{
					MovieName = s.MovieName,
					Actor = s.Actor,
					Country = s.Country,
					Director = s.Director,
					CoverPhoto = s.CoverPhoto,
					Year = s.Year.YearDate,
					GenreName = s.Genre.GenreName,
					MovieId = s.MovieId,
				})
				.Where(s => (s.GenreName.Contains(genreName))
				&& (s.Year.Contains(year)));


				return searchedMoviveByGenreAndYear;
			}

			return searchedMovive;
		}
	}
}
