using System.Collections.Generic;
using System.Linq;

namespace TripExpenseCalculator
{
    public class Trip
    {
        public List<Participant> Group { get; }
        public Trip()
        {
            this.Group = new List<Participant>();
        }

        public void SplitTheBill()
        {
            decimal tripTotalExpenses = this.Group.Sum(p => p.TotalExpenses);
            decimal tripPerParticipantShare = tripTotalExpenses / this.Group.Count;
            // If the participant owes money to the group, then the amount is positive.  
            // If the participant should collect money from the group, then the amount is negative.
            this.Group.ForEach(p => p.TripShareOutstanding = tripPerParticipantShare - p.TotalExpenses);
        }
    }
}
