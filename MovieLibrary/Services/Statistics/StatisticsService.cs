using MovieLibrary.Data;

namespace MovieLibrary.Services.Statistics
{
    public class StatisticsService : IStatisticsService
    {
        private readonly MovieLibraryDbContext data;

        public StatisticsService(MovieLibraryDbContext data) 
            => this.data = data;

        public StatisticsServiceModel Total()
        {
            var totalMovies = this.data.Movies.Count();
            var totalUsers = this.data.Users.Count();

            return new StatisticsServiceModel
            {
                TotalMovies = totalMovies,
                TotalUsers = totalUsers,
            };
        }
    }
}
