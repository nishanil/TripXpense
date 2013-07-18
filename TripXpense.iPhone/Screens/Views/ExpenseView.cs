using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Drawing;

namespace TripXpense.iPhone
{
	/// <summary>
	/// Expenses view.
	/// </summary>
	public class ExpensesView : UITableView
	{
		public ExpensesView ()
		{
			this.AutoresizingMask = UIViewAutoresizing.All;
			this.Source = new ExpensesViewSource ();
			this.BackgroundColor = UIColor.DarkGray;
		}

	}

	/// <summary>
	/// Expenses view source.
	/// </summary>
	public class ExpensesViewSource : UITableViewSource
	{
		static NSString cellIdentifier = new NSString ("cell");
		public override int RowsInSection (UITableView tableview, int section)
		{
			return 10;
		}
		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{

			ExpenseViewCell cell = tableView.DequeueReusableCell (cellIdentifier) as ExpenseViewCell;
			if (cell == null)
				cell = new ExpenseViewCell (cellIdentifier);
			cell.UpdateCell ("5/17/2013", "cab", "$40");

			return cell;
		}
	}

	/// <summary>
	/// Expense view cell.
	/// </summary>
	public class ExpenseViewCell : UITableViewCell
	{

		UILabel date, name, amount;
		public ExpenseViewCell (NSString cellId) 
			: base(UITableViewCellStyle.Default, cellId)
		{
			date = new UILabel () {

				Font = UIFont.FromName("AvenirNext-Medium", 10f),
				TextAlignment = UITextAlignment.Left,
				BackgroundColor = UIColor.Clear,
				TextColor = UIColor.White
				, 
			};

			name = new UILabel () {

				Font = UIFont.FromName("AvenirNext-Italic", 8f),
				TextAlignment = UITextAlignment.Left,
				BackgroundColor = UIColor.Clear,
				TextColor = UIColor.White

			};

			amount = new UILabel () {

				Font = UIFont.FromName("AvenirNext-Regular", 8f),
				TextAlignment = UITextAlignment.Right,
				BackgroundColor = UIColor.Clear,
				TextColor = UIColor.White
			};

			ContentView.Add (date);
			ContentView.Add (name);
			ContentView.Add (amount);
		}

		public void UpdateCell(string date, string name, string amount)
		{
			this.date.Text = date;
			this.name.Text = name;
			this.amount.Text = amount;
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			var dateFrame = new RectangleF (5, 1, 240, 20); 
			var nameFrame = new RectangleF (10, 20, 240, 20);
			var amountFrame = new RectangleF (nameFrame.Width, 1, 60, 20);
			date.Frame = dateFrame;
			name.Frame = nameFrame;
			amount.Frame = amountFrame;

		}
	}

}

