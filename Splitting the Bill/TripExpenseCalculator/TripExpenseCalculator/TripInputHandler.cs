using System;
using System.Collections.Generic;
using System.IO;

namespace TripExpenseCalculator
{
    public class TripInputHandler
    {
        /// <summary>
        /// Parses input file
        /// </summary>
        /// <param name="filePath">A valid text input file path</param>
        /// <returns>List of Trip data read from file</returns>
        public static List<Trip> ReadTripData(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !".TXT".Equals(Path.GetExtension(filePath), StringComparison.InvariantCultureIgnoreCase))
                throw new ApplicationException("You must provide a valid text input file");

            if (!File.Exists(filePath))
                throw new ApplicationException(string.Format("Input file does not exists [{0}]", filePath));

            // Assuming that the file has 'Read' access

            List<Trip> trips = new List<Trip>();
            string fileData = null;
            using (TextReader reader = new StreamReader(filePath))
            {
                fileData = reader.ReadToEnd();
                reader.Close();
            }

            if (string.IsNullOrEmpty(fileData))
                throw new ApplicationException("No trip data provided.");

            string[] tripRawData = fileData.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            if (tripRawData.Length == 0)
                throw new ApplicationException("No trip data provided.");

            int lineIndex = 0;
            int tripCounter = 1;
            while (lineIndex < tripRawData.Length)
            {
                // The information for each trip consists of a line containing a positive integer, n, 
                // the number of peopling participating in the camping trip
                int participantCount = 0;
                if (!int.TryParse(tripRawData[lineIndex++].Trim(), out participantCount))
                    throw new ApplicationException(string.Format("Trip [{0}] - Invalid data [Number of participant]", tripCounter));

                //  A single line containing 0 follows the information for the last camping trip
                if (participantCount == 0) break; // End of trips 

                if (lineIndex >= tripRawData.Length)
                    throw new ApplicationException(string.Format("Trip [{0}] - Missing data [Camping participant expense information]", tripCounter));

                Trip trip = new Trip();
                for (int participantIndex = 0; participantIndex < participantCount; participantIndex++)
                {
                    //Each groups consists of a line containing a positive integer, p, the number of receipts/charges for that participant
                    int receiptCount = 0;
                    if (!int.TryParse(tripRawData[lineIndex++].Trim(), out receiptCount))
                    {
                        throw new ApplicationException(string.Format("Trip [{0}] Participant [{1}] - Invalid data [Number of receipts/charges]"
                            , tripCounter, (participantIndex + 1)));
                    }

                    if (lineIndex >= tripRawData.Length)
                    {
                        throw new ApplicationException(string.Format("Trip [{0}] Participant [{1}] - Missing data [receipts/charges]"
                            , tripCounter, (participantIndex + 1)));
                    }

                    Participant participant = new Participant();
                    for (int receiptIndex = 0; receiptIndex < receiptCount; receiptIndex++)
                    {
                        if (lineIndex >= tripRawData.Length)
                        {
                            throw new ApplicationException(string.Format("Trip [{0}] Participant [{1}] Receipts/Charges [{2}] - Missing data [receipts/charges]"
                                , tripCounter, (participantIndex + 1), (receiptIndex + 1)));
                        }

                        decimal amount = 0;
                        if (!decimal.TryParse(tripRawData[lineIndex++].Trim(), System.Globalization.NumberStyles.AllowDecimalPoint, null, out amount))
                        {
                            throw new ApplicationException(string.Format("Trip [{0}] Participant [{1}] Receipts/Charges [{2}] - Invalid data [amount]"
                                , tripCounter, (participantIndex + 1), (receiptIndex + 1)));
                        }
                        participant.Expenses.Add(amount);
                    }
                    trip.Group.Add(participant);
                }
                trips.Add(trip);
            }
            return trips;
        }
    }
}
