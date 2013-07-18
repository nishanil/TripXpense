using System;

namespace TripXpense.Core.Model
{
    public class Expense : EntityBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public Guid TripId { get; set; }
    }
}
