using System.Collections.Generic;
using System.Linq;

namespace TripExpenseCalculator
{
    public class Participant
    {
        public List<decimal> Expenses { get; }
        public Participant()
        {
            this.Expenses = new List<decimal>();
        }

        public decimal TripShareOutstanding { get; set; }
        public decimal TotalExpenses
        {
            get
            {
                return this.Expenses.Sum();
            }
        }
    }
}
