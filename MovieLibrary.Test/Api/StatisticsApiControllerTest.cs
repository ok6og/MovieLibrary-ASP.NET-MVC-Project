using MovieLibrary.Controllers.Api;
using MovieLibrary.Test.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MovieLibrary.Test.Api
{
    public class StatisticsApiControllerTest
    {
        [Fact]
        public void GetStatisticsShouldReturnTotalStatistics()
        {
            var statisticsController = new StatisticsApiController(StatisticsServiceMock.Instance);

            var result = statisticsController.GetStatistics();

            Assert.NotNull(result);
            Assert.Equal(5, result.TotalMovies);
            Assert.Equal(10, result.TotalWatches);
            Assert.Equal(15,result.TotalUsers);
        }
    }
}
