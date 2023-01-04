﻿namespace Movies.Core.Contract.Movie
{
    using Movies.Core.ViewModels.Movie;

    public interface IMovieService
    {
        Task AddMovie(AddMovieViewModel addMovie, string imagePath);

    }
}
