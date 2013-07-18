using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Drawing;

namespace TripXpense.iPhone
{
	public class TripInfoScreenController : UIViewController
	{
		TripInfoView _expensesView;
		EditExpenseScreenController editExpenseController;
		public TripInfoScreenController ()
		{
			this.Title = "trip info";

		}
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			View = _expensesView = new TripInfoView ();

			var doneButton = new UIBarButtonItem (UIBarButtonSystemItem.Done, (s,e) => {


			});

			_expensesView.AddNewExpenseButton.TouchUpInside += (sender, e) => {
				if(editExpenseController == null)
					editExpenseController = new EditExpenseScreenController();
				NavigationController.PushViewController(editExpenseController, false);
			};

			NavigationItem.SetRightBarButtonItem(doneButton, false);

		}
	}

	/// <summary>
	/// Trip info view. Uses TripHeaderView & ExpensesView
	/// </summary>
	public class TripInfoView : UIView 
	{

		UILabel expensesLabel, total;
		public UIButton AddNewExpenseButton;
		public UIView ExpensesViewList, TripHeaderView;

		public TripInfoView ()
		{

			TripHeaderView = new TripInfoHeaderView ();
			ExpensesViewList = new ExpensesView ();

			this.BackgroundColor = UIColor.DarkGray;

			AddNewExpenseButton = UIButton.FromType (UIButtonType.ContactAdd);
			expensesLabel = new UILabel () {
				Font = UIFont.FromName("AvenirNext-Bold", 17f),
				TextAlignment = UITextAlignment.Left,
				BackgroundColor = UIColor.Clear,
				TextColor = UIColor.White,
				Text = "expenses"

			};

			total = new UILabel () {
				Font = UIFont.FromName("AvenirNext-Bold", 14f),
				TextAlignment = UITextAlignment.Left,
				BackgroundColor = UIColor.Clear,
				TextColor = UIColor.White,
				Text = "total: $260"

			};




			TripHeaderView.SizeToFit();
			ExpensesViewList.SizeToFit();
			expensesLabel.SizeToFit ();
			total.SizeToFit();

			AddSubviews (TripHeaderView, expensesLabel, AddNewExpenseButton, ExpensesViewList, total);
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			float x = 10f;

			var frame = TripHeaderView.Frame;
			frame.X = 0;
			frame.Y = 0;
			frame.Width = this.Bounds.Width;
			frame.Height = 80;
			TripHeaderView.Frame = frame;


			frame = expensesLabel.Frame;
			frame.X = x;
			frame.Y = TripHeaderView.Frame.Bottom + 5;
			frame.Width = this.Bounds.Width;
			expensesLabel.Frame = frame;

			frame = AddNewExpenseButton.Frame;
			frame.X = 280;
			frame.Y = expensesLabel.Frame.Y;
			AddNewExpenseButton.Frame = frame;

			frame = ExpensesViewList.Frame;
			frame.X = 0;
			frame.Y = expensesLabel.Frame.Bottom + 5;
			frame.Width = this.Bounds.Width;
			frame.Height = 300;
			ExpensesViewList.Frame = frame;

			frame = total.Frame;
			frame.X = x;
			frame.Y = ExpensesViewList.Frame.Bottom + 5;
			frame.Width = this.Bounds.Width;
			total.Frame = frame;

		}
	}

}

