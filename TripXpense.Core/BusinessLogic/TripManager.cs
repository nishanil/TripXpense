using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripXpense.Core.Model;
using TripXpense.Core.Repository;

namespace TripXpense.Core.BusinessLogic
{
    public class TripManager
    {
        private readonly ITripRepository _tripRepository = null;
        public TripManager()
        {
            _tripRepository = new TripXmlRepository();
        }

        public List<Trip> GetTrips()
        {
            return _tripRepository.GetTrips();
        }

        public void SaveTrips(List<Trip> trips)
        {
            _tripRepository.SaveTrips(trips);
        }

        public Trip GetTrip(Guid id)
        {
            return _tripRepository.GetTrip(id);
        }

        public void SaveTrip(Trip trip)
        {
            _tripRepository.SaveTrip(trip);
        }

        public void RemoveTrip(Trip trip)
        {
            _tripRepository.RemoveTrip(trip);
        }

        public void SaveExpense(Trip trip, Expense expense)
        {
            _tripRepository.SaveExpense(trip, expense);
        }

        public void RemoveExpense(Trip trip, Expense expense)
        {
            _tripRepository.RemoveExpense(trip, expense);
        }
    }
}
