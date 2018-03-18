using System;
using System.Collections.Generic;
using System.Text;

namespace Elastacloud.TimeSeries
{
    public static class MovingAverages
    {
      /// <summary>
      /// Returns the moving average of a time series 
      /// </summary>
      /// <param name="ts">input time series</param>
      /// <param name="size">size of time series</param>
      /// <returns>A collection of values based on the rolling mean</returns>
       public static IEnumerable<double> RollingMean(this DataFrame.Math.Data.TimeSeries ts, int size)
       {
          double[] buffer = new double[size];
          double[] output = new double[ts.Storage.Values.Count];
          int index = 0;
          for (int i = 0; i < ts.Storage.Values.Count; i++)
          {
             buffer[index] = ts.Storage.Values.Data[i] / size;
             double movingAverage = 0D;
             for (int j = 0; j < size; j++)
             {
                movingAverage += buffer[j];
             }
             output[i] = movingAverage;
             index = (index + 1) % size;
          }
          return output;
       }
    }
}
