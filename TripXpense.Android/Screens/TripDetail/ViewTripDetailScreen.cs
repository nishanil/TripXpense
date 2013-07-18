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
using TripXpense.Android.Screens.TripDetail;
using TripXpense.Core.BusinessLogic;
using TripXpense.Core.Model;

namespace TripXpense.Android.Screens
{
    [Activity(Label = "Trip Expenses")]
    public class ViewTripDetailScreen : Activity
    {
        private ListView _expensesView;
        private Trip _trip;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.ViewTripDetail);

            _expensesView = FindViewById<ListView>(Resource.Id.viewTripDetailListViewExpenses);

            var tripId = Intent.GetStringExtra("SelectedTripId");
            _trip =  new TripManager().GetTrip(new Guid(tripId));
            // In case the Trip was just created, intialize expenses
            
            UpdateUI();

            FindViewById<Button>(Resource.Id.viewTripDetaiButtonAdd).Click += (s, e) =>
            {
                var intent = new Intent(this, typeof(EditExpenseScreen));
                intent.PutExtra("SelectedTripId", _trip.Id.ToString());
                base.StartActivity(intent);
            };

            _expensesView.ItemClick += (s, e) =>
            {
                var intent = new Intent(this, typeof(EditExpenseScreen));
                intent.PutExtra("SelectedTripId", _trip.Id.ToString());
                intent.PutExtra("SelectedExpenseId", _trip.Expenses[e.Position].Id.ToString());
                base.StartActivity(intent);
            };
        }
        
        protected override void OnResume()
        {
            base.OnResume();
            _expensesView.Adapter = new ViewTripDetailExpenseListAdapter(this, _trip.Expenses);
        }


        private void UpdateUI()
        {
            FindViewById<TextView>(Resource.Id.viewTripDetailTextViewTripName).Text = _trip.Title;
            FindViewById<TextView>(Resource.Id.viewTripDetailTextViewDesc).Text = _trip.Description;

            FindViewById<TextView>(Resource.Id.viewTripDetailTextViewTripDate).Text = string.Format(
              "from {0} to {1} ", _trip.StartDate.ToString("d"), _trip.EndDate.ToString("d"));

            FindViewById<TextView>(Resource.Id.viewTripDetailTextViewBudget).Text = 
                string.Format("Budget: {0} ",_trip.Budget.ToString("C"));

            FindViewById<TextView>(Resource.Id.viewTripDetailTotalExpense).Text = string.Format("Total Expense: {0} ", _trip.TotalAmount.ToString("C")); ;

            
            
        }
    }
}