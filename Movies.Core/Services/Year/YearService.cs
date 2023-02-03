namespace Movies.Core.Services.Year
{
    using Movies.Core.Contract.Year;
    using Movies.Core.ViewModels.Year;
    using Movies.Infrastructure.Data;

    public class YearService : IYearService
    {
        private readonly ApplicationDbContext data;

        public YearService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public IEnumerable<AllYearViewModel> AllYears<T>()
        {
            var allYears = this.data.Years
                .Select(s => new AllYearViewModel
                {
                    Id = s.YearId,
                    YearDate = s.YearDate,
                });

            return allYears;
        }
    }
}
