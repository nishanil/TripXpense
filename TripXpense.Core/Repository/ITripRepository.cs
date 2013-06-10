using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripXpense.Core.Model;

namespace TripXpense.Core.Repository
{
    public interface ITripRepository
    {
        List<Trip> GetTrips();
        void SaveTrips(List<Trip> trips);
        Trip GetTrip(Guid id);
        void SaveTrip(Trip trip);
        void RemoveTrip(Trip trip);
        void SaveExpense(Trip trip, Expense expense);
        void RemoveExpense(Trip trip, Expense expense);
    }
}
