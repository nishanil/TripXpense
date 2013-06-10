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
using TripXpense.Core.BusinessLogic;
using TripXpense.Core.Model;

namespace TripXpense.Android.Screens
{
    [Activity(Label = "Edit Expenses")]
    public class EditExpenseScreen : Activity
    {
        private Trip _trip;
        private Expense _expense;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.EditExpense);

            var tripId = Intent.GetStringExtra("SelectedTripId");

            _trip = new TripManager().GetTrip(new Guid(tripId));
            var expenseId = Intent.GetStringExtra("SelectedExpenseId");

            if (!string.IsNullOrEmpty(expenseId))
                _expense = _trip.Expenses.FirstOrDefault((x) => x.Id == new Guid(expenseId));
            else
            {
                _expense = new Expense() {Date = DateTime.Now};
            }

            FindViewById<Button>(Resource.Id.editExpenseButtonSave).Click += (s, e) =>
            {
                UpdateExpense();
                new TripManager().SaveExpense(_trip, _expense);

                var intent = new Intent(this, typeof(ViewTripDetailScreen));
                intent.PutExtra("SelectedTripId", _trip.Id.ToString());

                base.StartActivity(intent);
            };

            FindViewById<Button>(Resource.Id.editExpenseButtonDelete).Click += (s, e) =>
            {
                
                new TripManager().RemoveExpense(_trip, _expense);

                var intent = new Intent(this, typeof(ViewTripDetailScreen));
                intent.PutExtra("SelectedTripId", _trip.Id.ToString());
                base.StartActivity(intent);
            };

            UpdateUI();
        }

        private void UpdateExpense()
        {
            _expense.Title = FindViewById<EditText>(Resource.Id.editExpenseEditTextTitle).Text;

            var date = FindViewById<DatePicker>(Resource.Id.editExpenseDatePickerDate);
            _expense.Date = new DateTime(date.Year, date.Month, date.DayOfMonth);

            _expense.Amount = Convert.ToDecimal(FindViewById<EditText>(Resource.Id.editExpenseEditTextAmount).Text);
        }

        private void UpdateUI()
        {
            FindViewById<TextView>(Resource.Id.editExpenseTextViewTripName).Text = _trip.Title;
            FindViewById<TextView>(Resource.Id.editExpenseTextViewTripDesc).Text = _trip.Description;

            FindViewById<TextView>(Resource.Id.editExpenseTextViewpTripDate).Text = string.Format(
              "from {0} to {1} ", _trip.StartDate.ToString("d"), _trip.EndDate.ToString("d"));

            FindViewById<TextView>(Resource.Id.editExpenseTextViewTripBudget).Text =
                string.Format("Budget: {0} ", _trip.Budget.ToString("C"));

            FindViewById<EditText>(Resource.Id.editExpenseEditTextTitle).Text = _expense.Title;

            FindViewById<DatePicker>(Resource.Id.editExpenseDatePickerDate).UpdateDate(_expense.Date.Year, _expense.Date.Month, _expense.Date.Day);

            FindViewById<EditText>(Resource.Id.editExpenseEditTextAmount).Text = _expense.Amount.ToString();

        }
    }
}