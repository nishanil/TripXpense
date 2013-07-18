using System;
using MonoTouch.UIKit;

namespace TripXpense.iPhone
{
	public class EditExpenseScreenController : UIViewController

	{
		EditExpenseView editExpenseView;
		public EditExpenseScreenController ()
		{
			this.Title = "trip info";
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var doneButton = new UIBarButtonItem(UIBarButtonSystemItem.Done, (s, e) => {
				NavigationController.PopViewControllerAnimated(false);
			});

			NavigationItem.SetRightBarButtonItem (doneButton, false);

			View = editExpenseView = new EditExpenseView ();
		}
	}

	public class EditExpenseView : UIView
	{
		UITextField expenseName,amount;
		ActionSheetDatePicker date;
		UIButton buttonDate;
		UILabel addExpenseLabel;
		public UIView TripHeaderView;
		public EditExpenseView ()
		{
			this.BackgroundColor = UIColor.DarkGray;

			TripHeaderView = new TripInfoHeaderView ();

			addExpenseLabel = new UILabel () {
				Font = UIFont.FromName("AvenirNext-Bold", 17f),
				TextAlignment = UITextAlignment.Left,
				BackgroundColor = UIColor.Clear,
				TextColor = UIColor.White,
				Text = "add new expense"

			};

			expenseName = new UITextField () { 
				Placeholder = "expense name?",
				BorderStyle = UITextBorderStyle.None,
				Font = UIFont.FromName("AvenirNext-Medium", 16f),
				TextColor = UIColor.Black,
				BackgroundColor = UIColor.White
			};

			date = new ActionSheetDatePicker (this);
			date.Title = "Choose Date:";
			date.DatePicker.Mode = UIDatePickerMode.Date;
			date.DatePicker.ValueChanged += (sender, e) => {
				DateTime selectedDate = (sender as UIDatePicker).Date;
				buttonDate.SetTitle(selectedDate.ToString("d"), UIControlState.Normal); 
			};

			amount = new UITextField () { 
				Placeholder = "$ amount?",
				BorderStyle = UITextBorderStyle.None,
				Font = UIFont.FromName("AvenirNext-Medium", 16f),
				TextColor = UIColor.Black,
				BackgroundColor = UIColor.White,
				KeyboardType = UIKeyboardType.NumberPad
			};




			buttonDate = UIButton.FromType (UIButtonType.Custom);
			buttonDate.SetTitle ("spent on Wednessday, Jul 17, 2013", UIControlState.Normal);
			buttonDate.SetTitleColor (UIColor.White, UIControlState.Normal);
			buttonDate.SetTitleColor (UIColor.LightGray, UIControlState.Highlighted);
			buttonDate.Font = UIFont.FromName ("AvenirNext-Medium", 16f);

			buttonDate.TouchUpInside += (sender, e) => {
				date.Show(); 
			};

			TripHeaderView.SizeToFit ();
			addExpenseLabel.SizeToFit();
			buttonDate.SizeToFit();
			expenseName.SizeToFit();
			amount.SizeToFit();

			AddSubviews (TripHeaderView, addExpenseLabel, expenseName, amount, buttonDate);
		}

		/// <Docs>Lays out subviews.</Docs>
		/// <para>The default implementation of this method does nothing on iOS 5.1 and earlier. Otherwise, the default
		/// implementation uses any constraints you have set to determine the size and position of any subviews.</para>
		/// <summary>
		/// Layouts the subviews.
		/// </summary>
		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			float x = 10f;
			float width = this.Bounds.Width - 20;

			var frame = TripHeaderView.Frame;
			frame.X = 0;
			frame.Y = 0;
			frame.Width = this.Bounds.Width;
			frame.Height = 80;
			TripHeaderView.Frame = frame;


			frame = addExpenseLabel.Frame;
			frame.X = x;
			frame.Y = TripHeaderView.Frame.Bottom + 5;
			frame.Width = this.Bounds.Width;
			addExpenseLabel.Frame = frame;

			frame = expenseName.Frame;
			frame.X = x;
			frame.Width = width;
			frame.Y = addExpenseLabel.Frame.Bottom + 5;
			expenseName.Frame = frame;

			frame = amount.Frame;
			frame.X = x;
			frame.Y = expenseName.Frame.Bottom + 10;
			frame.Width = width;
			amount.Frame = frame;

			frame = buttonDate.Frame;
			frame.X = x;
			frame.Y = amount.Frame.Bottom + 10;
			frame.Width = width;
			buttonDate.Frame = frame;


		}
	}


}

