using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripXpense.Core.DataLayer;
using TripXpense.Core.Model;

namespace TripXpense.Core.Repository
{
    public class TripXmlRepository : ITripRepository
    {
        private static List<Trip> _trips = TripXmlDatabase.ReadXml();

        public List<Trip> GetTrips()
        {
            return _trips;
        }


        public void SaveTrips(List<Trip> trips)
        {
            _trips = trips;
            SaveTrips();
        }

        private void SaveTrips()
        {
            TripXmlDatabase.WriteXml(_trips);
        }

        public Trip GetTrip(Guid id)
        {
            var trip = _trips != null ? _trips.FirstOrDefault((t) => t.Id == id) : null;
            if (trip != null && trip.Expenses == null)
                trip.Expenses = new List<Expense>();
            return trip;
        }


        public void SaveTrip(Trip trip)
        {
            if (trip == null) return;
            // If a trip is already present, Remove & add. Works like update
            if (GetTrip(trip.Id) != null)
                _trips.Remove(trip);
            _trips.Add(trip);
            SaveTrips();
        }


        public void RemoveTrip(Trip trip)
        {
            if (trip != null)
                _trips.Remove(trip);
            SaveTrips();
        }


        public void SaveExpense(Trip trip, Expense expense)
        {
            if (trip.Expenses.FirstOrDefault((x) => x.Id == expense.Id) != null)
                trip.Expenses.Remove(expense);
            trip.Expenses.Add(expense);
            SaveTrip(trip);

        }


        public void RemoveExpense(Trip trip, Expense expense)
        {
            trip.Expenses.Remove(expense);
            SaveTrip(trip);
        }
    }
}
