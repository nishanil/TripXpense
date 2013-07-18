using System;
using System.Collections.Generic;
using System.Linq;

namespace TripXpense.Core.Model
{
    public class Trip : EntityBase
    {

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Budget { get; set; }
        public List<Expense> Expenses { get; set; }

        public decimal TotalAmount
        {
            get
            {
                if (Expenses == null || Expenses.Count == 0)
                    return 0;
                else
                {
                    return Expenses.Sum(expense => expense.Amount);
                }
            }
        }
    }
}
