using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.Foundation;
using TripXpense.Core.Model;

namespace TripXpense.iPhone
{
	public class EditTripScreenController : UIViewController
	{
		EditTripScreenView editTripView; 
		TripInfoScreenController tripInfoController;
		Trip trip;
		/// <summary>
		/// Initializes a new instance of the <see cref="TripXpense.iPhone.EditTripScreenController"/> class.
		/// </summary>
		/// <param name="trip">Trip.</param>
		public EditTripScreenController (Trip selectedTrip)
		{
			string title = "add";
			if (selectedTrip == null) {
				this.trip = new Trip () { StartDate = DateTime.Now, EndDate = DateTime.Now };
			} else {
				this.trip = selectedTrip;
				title = "edit";
			}
			this.Title = string.Format ("{0} trip", title);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var doneButton = new UIBarButtonItem (UIBarButtonSystemItem.Done, (s,e) => {

			});
			NavigationItem.SetRightBarButtonItem (doneButton, false);

			View = editTripView = new EditTripScreenView (View.Frame, this.trip);

			editTripView.ButtonExpenses.TouchUpInside += (sender, e) => {
				if(tripInfoController ==null)
					tripInfoController = new TripInfoScreenController();
				this.NavigationController.PushViewController(tripInfoController, true);
			};

		}


	}



	public class EditTripScreenView : UIView
	{
		UITextField _tripNameTextField, _tripBudgetTextField;
		UITextView _tripDescTextField;
		private UIButton _btnStartDate, _btnEndDate;
		public UIButton ButtonExpenses, ButtonDeleteTrip;
		ActionSheetDatePicker actionSheetDatePickerStartDate, actionSheetDatePickerEndDate;
		UIScrollView _scrollView;

		public EditTripScreenView (RectangleF frame, Trip trip)
		{
			//this.Frame = frame;

			_scrollView = new UIScrollView (frame);
			this.BackgroundColor = UIColor.White;

			_tripNameTextField = new UITextField () { 
				Placeholder = "Trip Name",
				BorderStyle = UITextBorderStyle.RoundedRect,
			};

			_tripNameTextField = new UITextField () { 
				Placeholder = "trip name",
				BorderStyle = UITextBorderStyle.None,
				Font = UIFont.FromName("AvenirNext-Medium", 16f),
				TextColor = UIColor.Black,
				BackgroundColor = UIColor.White
			};

			_btnStartDate = UIButton.FromType (UIButtonType.Custom);
			_btnStartDate.SetTitle ("from Wednessday, Jul 17, 2013", UIControlState.Normal);
			_btnStartDate.SetTitleColor (UIColor.White, UIControlState.Normal);
			_btnStartDate.SetTitleColor (UIColor.LightGray, UIControlState.Highlighted);
			_btnStartDate.Font = UIFont.FromName ("AvenirNext-Medium", 16f);



			actionSheetDatePickerStartDate = new ActionSheetDatePicker (this);
			actionSheetDatePickerStartDate.Title = "Choose Date:";
			actionSheetDatePickerStartDate.DatePicker.Mode = UIDatePickerMode.Date;
			actionSheetDatePickerStartDate.DatePicker.ValueChanged += (sender, e) => {
				DateTime selectedDate = (sender as UIDatePicker).Date;
				_btnStartDate.SetTitle(selectedDate.ToString("D"), UIControlState.Normal); 
			};

			_btnStartDate.TouchUpInside += (sender, e) => {
				actionSheetDatePickerStartDate.Show(); 
			};

			_btnEndDate = UIButton.FromType (UIButtonType.Custom);
			_btnEndDate.SetTitle ("to Wednessday, Jul 17, 2013", UIControlState.Normal);
			_btnEndDate.SetTitleColor (UIColor.White, UIControlState.Normal);
			_btnEndDate.SetTitleColor (UIColor.LightGray, UIControlState.Highlighted);
			_btnEndDate.Font = UIFont.FromName ("AvenirNext-Medium", 16f);

			_btnEndDate.TouchUpInside += (sender, e) => {
				actionSheetDatePickerEndDate.Show();
			};
			actionSheetDatePickerEndDate = new ActionSheetDatePicker (this);
			actionSheetDatePickerEndDate.Title = "Choose Date:";
			actionSheetDatePickerEndDate.DatePicker.Mode = UIDatePickerMode.Date;
			actionSheetDatePickerEndDate.DatePicker.ValueChanged += (sender, e) => {
				DateTime selectedDate = (sender as UIDatePicker).Date;
				_btnEndDate.SetTitle(selectedDate.ToString("D"), UIControlState.Normal); 
			};


			_tripDescTextField = new UITextView () { 
				Font = UIFont.FromName("AvenirNext-Medium", 16f),
				TextColor = UIColor.Black,
				BackgroundColor = UIColor.White,
				Text = "Desc"
			};


			_tripBudgetTextField = new UITextField () { 
				Placeholder = "budget",
				BorderStyle = UITextBorderStyle.None,
				Font = UIFont.FromName("AvenirNext-Medium", 16f),
				TextColor = UIColor.Black,
				BackgroundColor = UIColor.White,
				KeyboardType = UIKeyboardType.NumberPad
			};

			ButtonExpenses = UIButton.FromType (UIButtonType.RoundedRect);
			ButtonExpenses.SetTitle ("expenses", UIControlState.Normal);


			ButtonDeleteTrip = UIButton.FromType (UIButtonType.RoundedRect);
			ButtonDeleteTrip.SetTitle ("delete trip", UIControlState.Normal);

			_tripNameTextField.SizeToFit ();
			_btnStartDate.SizeToFit ();
			_btnEndDate.SizeToFit ();
			_tripDescTextField.SizeToFit ();
			_tripBudgetTextField.SizeToFit ();
			ButtonExpenses.SizeToFit ();

			ButtonDeleteTrip.SizeToFit();

			_tripNameTextField.ShouldReturn += (t) => {
				t.ResignFirstResponder();
				return true;
			};


			// set content size to default for now
			//TODO: implement observer and set accordingly
			_scrollView.ContentSize = new SizeF (frame.Width, frame.Height);

			_scrollView.AddSubviews (_tripNameTextField, _btnStartDate, 
			                         _btnEndDate, _tripDescTextField,
			                         _tripBudgetTextField, ButtonExpenses, ButtonDeleteTrip);

			_scrollView.SizeToFit ();
			_scrollView.BackgroundColor = this.BackgroundColor = UIColor.DarkGray;

			this.AddSubview ( _scrollView);
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			float x = 10f;
			var tempFrame = _tripNameTextField.Frame;
			tempFrame.X = x;
			tempFrame.Width = this.Bounds.Width - 20;
			_tripNameTextField.Frame = tempFrame;

			tempFrame = _btnStartDate.Frame;
			tempFrame.X = x;
			tempFrame.Width = this.Bounds.Width - 20;
			tempFrame.Y = _tripNameTextField.Frame.Bottom + 10;
			_btnStartDate.Frame = tempFrame;

			tempFrame = _btnEndDate.Frame;
			tempFrame.X = x;
			tempFrame.Width = this.Bounds.Width - 20;
			tempFrame.Y = _btnStartDate.Frame.Bottom + 10;
			_btnEndDate.Frame = tempFrame;


			tempFrame = _tripDescTextField.Frame;
			tempFrame.X = x;
			tempFrame.Width = this.Bounds.Width - 20;
			tempFrame.Y = _btnEndDate.Frame.Bottom + 10;
			tempFrame.Height = 80;
			_tripDescTextField.Frame = tempFrame;

			tempFrame = _tripBudgetTextField.Frame;
			tempFrame.X = x;
			tempFrame.Width = this.Bounds.Width - 20;
			tempFrame.Y = _tripDescTextField.Frame.Bottom + 10;
			_tripBudgetTextField.Frame = tempFrame;

			tempFrame = ButtonExpenses.Frame;
			tempFrame.X = x;
			tempFrame.Width = this.Bounds.Width - 20;
			tempFrame.Y = _tripBudgetTextField.Frame.Bottom + 10;
			ButtonExpenses.Frame = tempFrame;

			tempFrame = ButtonDeleteTrip.Frame;
			tempFrame.X = x;
			tempFrame.Width = this.Bounds.Width - 20;
			tempFrame.Y = ButtonExpenses.Frame.Bottom + 10;
			ButtonDeleteTrip.Frame = tempFrame;
		}
	}


}

