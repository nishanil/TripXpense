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
using TripXpense.Core.BusinessLogic;

namespace TripXpense.Android.Screens
{
    [Activity(Label = "TripXpense", MainLauncher = true, Icon = "@drawable/icon")]
    public class HomeScreen : Activity
    {
        private ListView _tripsView;
        private List<Trip> _trips;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Home);

            FindViewById<Button>(Resource.Id.homebuttonAddTrip).Click += (s, e) =>
                {
                    var intent = new Intent(this, typeof(EditTripScreen));
                    base.StartActivity(intent);
                };

            _tripsView = FindViewById<ListView>(Resource.Id.homeListViewTrips);
            if (_tripsView != null)
            {
                _tripsView.ItemClick += (s, e) =>
                    {
                        var intent = new Intent(this, typeof(EditTripScreen));
                        intent.PutExtra("SelectedTripId", _trips[e.Position].Id.ToString());
                        base.StartActivity(intent);
                    };
            }

        }

        protected override void OnResume()
        {
            base.OnResume();
            _trips = new TripManager().GetTrips();
            _tripsView.Adapter = new HomeTripsListAdapter(this, _trips);
        }
    }
}