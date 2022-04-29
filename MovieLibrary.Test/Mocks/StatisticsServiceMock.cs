using Moq;
using MovieLibrary.Services.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Test.Mocks
{
    public static class StatisticsServiceMock
    {
        public static IStatisticsService Instance
        {
            get
            {
                var statisticsServiceMock = new Mock<IStatisticsService>();

                statisticsServiceMock
                    .Setup(s => s.Total())
                    .Returns(new StatisticsServiceModel
                    {
                        TotalMovies = 5,
                        TotalUsers = 15,
                        TotalWatches = 10
                    });
                return statisticsServiceMock.Object;
                
            }
        }
            
    }
}
