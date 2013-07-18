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
    [Activity(Label = "Edit Trip Details")]
    public class EditTripScreen : Activity
    {
        private Trip _trip;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.EditTrip);

            var tripId = Intent.GetStringExtra("SelectedTripId");
            _trip = String.IsNullOrEmpty(tripId)
                        ? new Trip() { StartDate = DateTime.Now, EndDate = DateTime.Now }
                        : new TripManager().GetTrip(new Guid(tripId));

            UpdateUI();

            FindViewById<Button>(Resource.Id.editTripButtonSave).Click += (s, e) =>
                {
                    UpdateTrip();
                    new TripManager().SaveTrip(_trip);

                    var intent = new Intent(this, typeof(ViewTripDetailScreen));
                    intent.PutExtra("SelectedTripId", _trip.Id.ToString());

                    base.StartActivity(intent);
                };

            FindViewById<Button>(Resource.Id.editTripButtonDelete).Click += (s, e) =>
            {
               
                new TripManager().RemoveTrip(_trip);
                var intent = new Intent(this, typeof(HomeScreen));
                base.StartActivity(intent);
            };

        }

        private void UpdateUI()
        {
            FindViewById<TextView>(Resource.Id.editTripEditTextTripName).Text = _trip.Title;
            FindViewById<DatePicker>(Resource.Id.editTripDatePickerStart).UpdateDate(_trip.StartDate.Year, _trip.StartDate.Month, _trip.StartDate.Day);
            FindViewById<DatePicker>(Resource.Id.editTripDatePickerStart).UpdateDate(_trip.EndDate.Year, _trip.EndDate.Month, _trip.EndDate.Day);
            FindViewById<EditText>(Resource.Id.editTripEditTextBudget).Text = _trip.Budget.ToString();
            FindViewById<EditText>(Resource.Id.editTripEditTextDesc).Text = _trip.Description;
        }

        private void UpdateTrip()
        {
            _trip.Title = FindViewById<TextView>(Resource.Id.editTripEditTextTripName).Text;

            var startDate = FindViewById<DatePicker>(Resource.Id.editTripDatePickerStart);
            _trip.StartDate = new DateTime(startDate.Year, startDate.Month, startDate.DayOfMonth);
            var endDate = FindViewById<DatePicker>(Resource.Id.editTripDatePickerStart);
            _trip.EndDate = new DateTime(endDate.Year, endDate.Month, endDate.DayOfMonth);
            _trip.Budget = Convert.ToDecimal(FindViewById<EditText>(Resource.Id.editTripEditTextBudget).Text);
            _trip.Description = FindViewById<EditText>(Resource.Id.editTripEditTextDesc).Text;

        }
    }
}