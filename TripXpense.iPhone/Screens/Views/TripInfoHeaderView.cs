using System;
using MonoTouch.UIKit;

namespace TripXpense.iPhone
{
	public class TripInfoHeaderView : UIView
	{
		UILabel tripName, tripDescription, tripDates, tripBudget, total;
		public TripInfoHeaderView ()
		{
			this.BackgroundColor = UIColor.DarkGray;
			tripName = new UILabel () {
				Font = UIFont.FromName("AvenirNext-Medium", 16f),
				TextAlignment = UITextAlignment.Left,
				BackgroundColor = UIColor.Clear,
				TextColor = UIColor.White,
				Text = "san francisco"

			};

			tripDescription = new UILabel () {
				Font = UIFont.FromName("AvenirNext-Italic", 10f),
				TextAlignment = UITextAlignment.Left,
				BackgroundColor = UIColor.Clear,
				TextColor = UIColor.White,
				Text = "company meeting & training"

			};

			tripDates = new UILabel () {
				Font = UIFont.FromName("AvenirNext-Medium", 10f),
				TextAlignment = UITextAlignment.Left,
				BackgroundColor = UIColor.Clear,
				TextColor = UIColor.White,
				Text = "from 6/6/2012 to 6/6/2013"

			};

			tripBudget = new UILabel () {
				Font = UIFont.FromName("AvenirNext-Bold", 14f),
				TextAlignment = UITextAlignment.Left,
				BackgroundColor = UIColor.Clear,
				TextColor = UIColor.White,
				Text = "budget: $500"

			};

			tripName.SizeToFit ();
			tripDescription.SizeToFit ();
			tripDates.SizeToFit ();
			tripBudget.SizeToFit ();

			AddSubviews (tripName, tripDescription, tripDates, tripBudget);

		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			float x = 10f;
			var frame = tripName.Frame;
			frame.X = x;
			frame.Y = 10;
			frame.Width = this.Bounds.Width;
			tripName.Frame = frame;


			frame = tripDescription.Frame;
			frame.X = x;
			frame.Y = tripName.Frame.Bottom + 2;
			frame.Width = this.Bounds.Width;
			tripDescription.Frame = frame;

			frame = tripDates.Frame;
			frame.X = x + 2;
			frame.Y = tripDescription.Frame.Bottom + 2;
			frame.Width = this.Bounds.Width;
			tripDates.Frame = frame;

			frame = tripBudget.Frame;
			frame.X = x;
			frame.Y = tripDates.Frame.Bottom + 2;
			frame.Width = this.Bounds.Width;
			tripBudget.Frame = frame;
		}

	}
}

