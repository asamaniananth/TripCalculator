using System;
using System.Collections.Generic;

namespace TripExpenseCalculator
{
    class Program
    {

        static void Main(string[] args)
        {
            try
            {
                if (args.Length == 0 || string.IsNullOrEmpty(args[0]))
                    throw new ApplicationException("You must provide a valid text input file");

                string inputFilePath = args[0];

                //string inputFilePath = @"W:\Personal\Ananth\Splitting the Bill\Trip Data\Trip1.txt";
                List<Trip> trips = TripInputHandler.ReadTripData(inputFilePath);
                if (trips == null || trips.Count == 0)
                    throw new ApplicationException("Unable to read input file or No trip data provided.");

                trips.ForEach(t => t.SplitTheBill());
                TripOutputHandler.WriteTripData(trips, string.Format("{0}.out", inputFilePath));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Read();
            }

        }
    }
}
