using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TripXpense.Core.Model;

namespace TripXpense.Android.Screens
{
    public class HomeTripsListAdapter : BaseAdapter<Trip>
    {
        List<Trip> _trips;
        Activity _context;
        public HomeTripsListAdapter(Activity context, List<Trip> trips)
            : base()
        {
            this._context = context;
            this._trips = trips;
        }

        public override Trip this[int position]
        {
            get { return _trips[position]; }
        }

        public override int Count
        {
            get { return _trips.Count; }
        }

        public override long GetItemId(int position)
        {
            return  position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = _trips[position];
            View view = convertView ?? _context.LayoutInflater.Inflate(Resource.Layout.HomeTripsList, null);
            view.FindViewById<TextView>(Resource.Id.TripName).Text = item.Title;
            view.FindViewById<TextView>(Resource.Id.TripDate).Text = string.Format(
                "from {0} to {1} ", item.StartDate.ToString("d"), item.EndDate.ToString("d"));
            view.FindViewById<TextView>(Resource.Id.TripAmount).Text = item.TotalAmount.ToString("C");
            return view;
        }
    }
}