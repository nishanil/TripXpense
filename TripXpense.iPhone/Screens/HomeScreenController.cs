using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Drawing;
using System.Collections.Generic;
using TripXpense.Core.Model;
using TripXpense.Core.BusinessLogic;

namespace TripXpense.iPhone
{
	public class HomeScreenController : UIViewController
	{
		UITableView tripListView;
		EditTripScreenController  editTripScreenController;

		List<Trip> trips;
		Trip selectedTrip;

		public HomeScreenController ()
		{
			this.Title = "your trips";
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var addTripBarButton = new UIBarButtonItem (UIBarButtonSystemItem.Add, 
			                                            (s,e) => {
				if(editTripScreenController==null)
					editTripScreenController = new EditTripScreenController(null);

				this.NavigationController.PushViewController(editTripScreenController, true);

			});

			tripListView = new UITableView (View.Bounds);
			tripListView.AutoresizingMask = UIViewAutoresizing.All;
			tripListView.BackgroundColor = UIColor.DarkGray;


			NavigationItem.SetRightBarButtonItem (addTripBarButton, false);

			//tripListView
			Add (tripListView);
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			trips = new TripManager ().GetTrips ();
			if (trips.Count == 0)
				trips.Add (new Trip () { Title = "Bangalore", StartDate = DateTime.Now, EndDate = DateTime.Now });
			var tripSource = new TripViewSource (trips);

			tripSource.TripSelectionChanged += (t) => {

				editTripScreenController = new EditTripScreenController(t);

				this.NavigationController.PushViewController(editTripScreenController, true);
			};

			tripListView.Source = tripSource;
		}
	}


	public class TripViewCell : UITableViewCell
	{
		UILabel _tripHeading, _tripDescription, _tripPrice;
		public TripViewCell (NSString cellId) 
			: base(UITableViewCellStyle.Default, cellId)
		{
			_tripHeading = new UILabel () {

				Font = UIFont.FromName("AvenirNext-Medium", 16f),
				TextAlignment = UITextAlignment.Left,
				BackgroundColor = UIColor.Clear,
				TextColor = UIColor.White
				, 
			};

			_tripDescription = new UILabel () {

				Font = UIFont.FromName("AvenirNext-Italic", 10f),
				TextAlignment = UITextAlignment.Left,
				BackgroundColor = UIColor.Clear,
				TextColor = UIColor.White

			};

			_tripPrice = new UILabel () {

				Font = UIFont.FromName("AvenirNext-Regular", 16f),
				TextAlignment = UITextAlignment.Right,
				BackgroundColor = UIColor.Clear,
				TextColor = UIColor.White
			};

			ContentView.Add (_tripHeading);
			ContentView.Add (_tripDescription);
			ContentView.Add (_tripPrice);


		}

		public void UpdateCell(string tripHeading, string tripDescription, string tripPrice)
		{
			_tripHeading.Text = tripHeading;
			_tripDescription.Text = tripDescription;
			_tripPrice.Text = tripPrice;
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			var headFrame = new RectangleF (5, 1, 240, 20); 
			var descFrame = new RectangleF (10, 20, 240, 20);
			var priceFrame = new RectangleF (descFrame.Width, 1, 60, 20);
			_tripHeading.Frame = headFrame;
			_tripDescription.Frame = descFrame;
			_tripPrice.Frame = priceFrame;
		
		}
	}


	public class TripViewSource : UITableViewSource
	{
		List<Trip> trips;

		public delegate void SelectedTripEventHandler (Trip trip);

		public event SelectedTripEventHandler TripSelectionChanged;

		public TripViewSource (List<Trip> trips)
		{
			this.trips = trips;
		}

		static NSString cellIdentifier = new NSString ("cell");
		public override int RowsInSection (UITableView tableview, int section)
		{
			if(trips!=null)
			return trips.Count;
			return 0;
		}
		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{

			TripViewCell cell = tableView.DequeueReusableCell (cellIdentifier) as TripViewCell;
			if (cell == null)
				cell = new TripViewCell (cellIdentifier);
			if(trips!=null && trips.Count > 0)
			{
				var trip = trips[indexPath.Row];
				cell.UpdateCell (trip.Title, string.Format("from {0} to {1}", 
				                                           trip.StartDate.ToString("D"), 
				                                           trip.EndDate.ToString("D")), 
				                 trip.TotalAmount.ToString("C"));
			}
			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			if (trips != null) {
				var selectedTrip = trips [indexPath.Row];
				TripSelectionChanged (selectedTrip);
			}
			tableView.DeselectRow (indexPath, true); // iOS convention is to remove the highlight
		}
	}

}

