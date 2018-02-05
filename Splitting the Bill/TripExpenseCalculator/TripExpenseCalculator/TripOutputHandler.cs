using System;
using System.Collections.Generic;
using System.IO;

namespace TripExpenseCalculator
{
    public class TripOutputHandler
    {
        public static void WriteTripData(List<Trip> trips, string outputFilePath)
        {
            if (trips == null || trips.Count == 0)
                throw new ApplicationException("No trip data provided to output.");

            if (string.IsNullOrEmpty(outputFilePath))
                throw new ApplicationException("You must provide a valid output file path");

            using (TextWriter writer = new StreamWriter(outputFilePath))
            {
                trips.ForEach(t =>
                {
                    // output one line per participant indicating how much he/she must pay or be paid rounded to the nearest cent.
                    t.Group.ForEach(p =>
                    {
                        //Negative amounts should be denoted by enclosing the amount in brackets.
                        decimal amount = Math.Round(p.TripShareOutstanding, 2);
                        writer.WriteLine(p.TripShareOutstanding < 0
                            ? string.Format("(${0})", -amount)
                            : string.Format("${0}", amount));
                    });
                    writer.WriteLine();
                });
                writer.Close();
            }
        }
    }
}
