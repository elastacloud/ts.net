using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DataFrame.Math.Data;

namespace Elastacloud.TimeSeries
{
    public class FileToTimeSeries
    {
      /// <summary>
      /// Reads a csv file and returns a time series
      /// </summary>
      /// <param name="fileName">The name of the csv file</param>
      /// <param name="seriesName">The name of the time series</param>
      /// <param name="hasHeader">whether the time series has a header or not</param>
      /// <returns>A DataFrame.Math.Data.TimeSeries object</returns>
      public static async Task<DataFrame.Math.Data.TimeSeries> FromCsv(string fileName, string seriesName, bool hasHeader = false)
       {
          var fileContents = await File.ReadAllLinesAsync(fileName);
          int index = 0;
          var timePart = new List<DateTime>();
          var valuePart = new List<double>();
          foreach (string line in fileContents)
          {
             if (hasHeader && index == 0)
             {
                continue;
             }
             string[] lineParts = line.Split(",");
             if (lineParts.Length > 2)
             {
                throw new ApplicationException("unable to parse file. Needs to be in the format of datetime and value.");
             }
             // Add the date to the collection
             DateTime dt = DateTime.MaxValue;
             bool dtAvailable = DateTime.TryParse(lineParts[0], out dt);
             if (!dtAvailable)
             {
                throw new ApplicationException($"invalid date reference at line {index}");
             }
             timePart.Add(dt);
             // Add the value to the collection
             double val = 0;
             bool valAvailable = Double.TryParse(lineParts[1], out val);
             if (!valAvailable)
             {
                throw new ApplicationException($"invalid value reference at line {index}");
             }
             valuePart.Add(val);
            index++;
          }
          return new DataFrame.Math.Data.TimeSeries(new TimeSeriesStorage(seriesName, timePart, valuePart));
      }
    }
}
