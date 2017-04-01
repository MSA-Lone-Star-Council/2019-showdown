using System;
using Admin.Common;
using Admin.Common.API.Entities;
using UIKit;

namespace Admin.iOS
{
	public partial class LocationViewController : UIViewController, ILocationDetailView
	{
		LocationDetailPresenter Presenter;
		
		public LocationViewController(Location row)
		{
			var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
			Presenter = new LocationDetailPresenter(row, appDelegate.BackendClient);
		}

		partial void AdditionalSetup()
		{
			UpdateButton.TouchUpInside += async (sender, e) => await Presenter.Save();
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			Presenter.TakeView(this);
		}

		public override void ViewDidDisappear(bool animated)
		{
			base.ViewDidDisappear(animated);
			Presenter.RemoveView();
		}

		Location ILocationDetailView.Location
		{
			get
			{
				return new Location()
				{
					Name = NameField.Text,
					Address = AddressField.Text,
					Notes = NotesField.Text
				};
			}

			set
			{
				NameField.Text = value.Name;
				AddressField.Text = value.Address;
				NotesField.Text = value.Notes;
			}
		}

		bool ILocationDetailView.LocationSaving
		{
			set
			{
				UpdateButton.Enabled = !value;
				UpdateButton.BackgroundColor = UpdateButton.Enabled ? UIColor.Green : UIColor.Gray;
			}
		}
	}
}
