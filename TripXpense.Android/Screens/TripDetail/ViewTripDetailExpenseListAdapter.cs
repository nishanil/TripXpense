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

namespace TripXpense.Android.Screens.TripDetail
{
    public class ViewTripDetailExpenseListAdapter : BaseAdapter<Expense>
    {
        List<Expense> _expenses ;
        private Activity _context;
        public ViewTripDetailExpenseListAdapter(Activity context, List<Expense> expenses)
        {
            this._expenses = expenses;
            this._context = context;
        }

        public override Expense this[int position]
        {
            get { return _expenses[position]; }
        }

        public override int Count
        {
            get { return _expenses.Count; }
        }

        public override long GetItemId(int position)
        {
            return  position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = _expenses[position];
            
            View view = convertView ?? _context.LayoutInflater.Inflate(Resource.Layout.ViewTripDetailExpenseList, null);
            view.FindViewById<TextView>(Resource.Id.viewTripDetailExpenseDate).Text =
               item.Date.ToString("d");
            view.FindViewById<TextView>(Resource.Id.viewTripDetailExpenseName).Text = item.Title;
            view.FindViewById<TextView>(Resource.Id.viewTripDetailExpenseAmount).Text = item.Amount.ToString("C");
            return view;
        }
    }
}