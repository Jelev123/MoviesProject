namespace Movies.Core.Services.Genre
{
    using Movies.Core.Contract.Genre;
    using Movies.Core.ViewModels.Genre;
    using Movies.Infrastructure.Data;
    using System.Collections.Generic;

    public class GenreService : IGenreService
    {
        private readonly ApplicationDbContext data;

        public GenreService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public IEnumerable<AllGenreViewModel> AllGenres<T>()
        {

            var allGenres = this.data.Genres
                .Select(s => new AllGenreViewModel
                {
                    Id = s.GenreId,
                    GenreName = s.GenreName,
                });

            return allGenres;
        }
    }
}
