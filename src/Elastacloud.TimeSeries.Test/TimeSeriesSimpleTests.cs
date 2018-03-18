using System.Linq;
using DataFrame.Math.Data;
using Xunit;

namespace Elastacloud.TimeSeries.Test
{
    public class TestTimeSeriesFunctions
    {
        [Fact]
        public async void LoadFile_CheckNumberOfRows_EqualToFrequencyCount()
        {
           var dailyTemps = await FileToTimeSeries.FromCsv(@"..\..\data\daily-minimum-temperatures-in-me.csv",
              "Melbourne Temperatures", true);
           Assert.Equal(Frequency.Day, dailyTemps.Frequency);
        }

       [Fact]
       public async void LoadFile_CheckMovingAverageValues_EqualsPeriodDivide()
       {
          var dailyTemps = await FileToTimeSeries.FromCsv(@"..\..\data\daily-minimum-temperatures-in-me.csv",
             "Melbourne Temperatures", true);
          int count = dailyTemps.Storage.Values.Count;
          int size = 4;
          var output = dailyTemps.RollingMean(4);
          Assert.Equal(count / size, output.Count());
       }
   }
}
