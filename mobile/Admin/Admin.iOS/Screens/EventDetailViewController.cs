using System;
using System.Collections.Generic;
using Admin.Common;
using Admin.Common.API.Entities;
using UIKit;
using Masonry;
using Foundation;
using Common.Common;
using Common.iOS;

namespace Admin.iOS
{
	public partial class EventDetailViewController : UIViewController, IEventDetailView
	{
		EventDetailPresenter Presenter;

		public EventDetailViewController(Event e)
		{
			var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
			Presenter = new EventDetailPresenter(e, appDelegate.BackendClient);
		}

		partial void AdditonalSetup()
		{
			LocationPicker.Model = new LocationPickerModel(Presenter, LocationField);
			AudiencePicker.Model = new AudiencePickerModel(AudienceField);
			UpdateButton.TouchUpInside += async (sender, e) => await Presenter.Save();
		}

		public async override void ViewWillAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			Presenter.TakeView(this);
			var loadingTask = Presenter.OnBegin();
			await loadingTask;
		}


		int IEventDetailView.SelectedLocationIndex
		{
			get
			{
				return (int)LocationPicker.SelectedRowInComponent(0);	
			}

			set
			{
				LocationPicker.Select(value, 0, true);
				LocationField.Text = LocationPicker.Model.GetTitle(LocationPicker, value, 0);
			}
		}

		bool IEventDetailView.LocationLoading
		{
			set
			{
				LocationField.Enabled = !value;
				UpdateButton.Enabled = !value;
				UpdateButton.BackgroundColor = UpdateButton.Enabled ? UIColor.Green : UIColor.Gray;

				LocationPicker.ReloadAllComponents();
			}
		}

		Event IEventDetailView.Event
		{
			set
			{
				TitleField.Text = value.Title;
				DescriptionField.Text = value.Description;

				AudienceField.Text = value.Audience;
				AudiencePicker.Select(AudiencePickerModel.StringToIndex(value.Audience), 0, false);

				StartTimeField.Text = Utilities.FormatEventTime(value.StartTime);
				StartTimePicker.Date = IOSHelpers.ConvertToNSDate(value.StartTime);

				EndTimeField.Text = Utilities.FormatEventTime(value.EndTime);
				EndTimePicker.Date = IOSHelpers.ConvertToNSDate(value.EndTime);

				// Location will be set by presenter
			}
			get
			{
				return new Event()
				{
					// Presenter already has ID
					Title = TitleField.Text,
					Description = DescriptionField.Text,
					Audience = AudienceField.Text,
					StartTime = IOSHelpers.Convert(StartTimePicker.Date),
					EndTime = IOSHelpers.Convert(EndTimePicker.Date),
					// Presenter will figure out location from IEventDetailView.SelectedLocationIndex
				};
			}
		}


		public class LocationPickerModel : UIPickerViewModel
		{
			UITextField _owner;
			EventDetailPresenter _presenter;

			public LocationPickerModel(EventDetailPresenter presenter, UITextField owner)
			{
				this._owner = owner;
				this._presenter = presenter;
			}

			public override nint GetComponentCount(UIPickerView pickerView)
			{
				return 1;
			}

			public override string GetTitle(UIPickerView pickerView, nint row, nint component)
			{
				return _presenter.GetLocationName((int)row);
			}

			public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
			{
				return _presenter.GetLocationCount();
			}

			public override void Selected(UIPickerView pickerView, nint row, nint component)
			{
				_owner.Text = GetTitle(pickerView, row, component);
			}
		}

		public class AudiencePickerModel : UIPickerViewModel
		{
			UITextField _owner;
			EventDetailPresenter _presenter;

			public AudiencePickerModel(UITextField owner)
			{
				this._owner = owner;
			}

			public override nint GetComponentCount(UIPickerView pickerView)
			{
				return 1;
			}

			public override string GetTitle(UIPickerView pickerView, nint row, nint component)
			{
				return IndexToString((int)row);
			}

			public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
			{
				return 3;
			}

			public override void Selected(UIPickerView pickerView, nint row, nint component)
			{
				_owner.Text = GetTitle(pickerView, row, component);
			}

			public static int StringToIndex(string audienceString)
			{
				switch (audienceString)
				{
					case "general": return 0;
					case "brothers": return 1;
					case "sisters": return 2;
				}
				return -1;
			}

			public static string IndexToString(int index)
			{
				switch (index)
				{
					case 0: return "general";
					case 1: return "brothers";
					case 2: return "sisters";
				}
				return "";
			}
		}

	}
}
