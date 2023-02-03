namespace Movies.Core.Contract.Year
{
    using Movies.Core.ViewModels.Year;
    using System.Collections.Generic;

    public interface IYearService
    {
        IEnumerable<AllYearViewModel> AllYears<T>();

    }
}
