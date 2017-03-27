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
	public class EventDetailViewController : UIViewController, IEventDetailView
	{
		EventDetailPresenter presenter;

		UITextField TitleField = new UITextField() { 
			Font = UIFont.FromName("SanFranciscoDisplay-Bold", 20),
			TextAlignment = UITextAlignment.Center,
			BorderStyle = UITextBorderStyle.RoundedRect
		};

		UIPickerView AudiencePicker = new UIPickerView();
		UITextField AudienceField;

		UITextView DescriptionField = new UITextView();

		UIDatePicker StartTimePicker = new UIDatePicker();
		UITextField StartTimeField;

		UIDatePicker EndTImePicker = new UIDatePicker();
		UITextField EndTimeField;

		UIPickerView LocationPicker = new UIPickerView();
		UITextField LocationField;

		UIButton UpdateButton = new UIButton() { BackgroundColor = UIColor.Green };
		UIButton DeleteButton = new UIButton() { BackgroundColor = UIColor.Red };

		public Event Event { get; set; }



		List<string> IEventDetailView.LocationOptions
		{
			set
			{
				(LocationPicker.Model as LocationPickerModel).locations = value;
			}
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
				LocationField.Text = (LocationPicker.Model as LocationPickerModel).locations[value];
			}
		}

		bool IEventDetailView.LocationLoading
		{
			set
			{
				LocationField.Enabled = !value;
				UpdateButton.Enabled = !value;

				UpdateButton.BackgroundColor = UpdateButton.Enabled ? UIColor.Green : UIColor.Gray;
			}
		}

		public EventDetailViewController(Event e)
		{
			var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
			presenter = new EventDetailPresenter(appDelegate.BackendClient) { Event = e };
			Event = e;
			presenter.TakeView(this);

		}

		public async override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			presenter.TakeView(this);
			await presenter.OnBegin();
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
			var navController = appDelegate.Window.RootViewController as UINavigationController;

			NavigationItem.RightBarButtonItem = new UIBarButtonItem(
				"Dismiss Keyboard",
				UIBarButtonItemStyle.Plain,
				(sender, e) => InvokeOnMainThread(() => View.EndEditing(true))
			);


			StartTimeField = new UITextField() { 
				InputView = StartTimePicker, 
				Delegate = new UneditableTextField(),
				TextAlignment = UITextAlignment.Center,
				BorderStyle = UITextBorderStyle.RoundedRect,
			};
			StartTimePicker.ValueChanged += (sender, e) =>
			{
				UIDatePicker picker = sender as UIDatePicker;
				StartTimeField.Text = Utilities.FormatDateTime(IOSHelpers.Convert(picker.Date));
			};

			EndTimeField = new UITextField() { 
				InputView = EndTImePicker, 
				Delegate = new UneditableTextField(),
				TextAlignment = UITextAlignment.Center,
				BorderStyle = UITextBorderStyle.RoundedRect
			};
			EndTImePicker.ValueChanged += (sender, e) =>
			{
				UIDatePicker picker = sender as UIDatePicker;
				EndTimeField.Text = Utilities.FormatDateTime(IOSHelpers.Convert(picker.Date));
			};

			AudienceField = new UITextField()
			{
				InputView = AudiencePicker,
				Delegate = new UneditableTextField(),
				TextAlignment = UITextAlignment.Center,
				BorderStyle = UITextBorderStyle.RoundedRect
			};
			AudiencePicker.Model = new AudiencePickerModel(AudienceField);

			LocationField = new UITextField() {
				InputView = LocationPicker, 
				Delegate = new UneditableTextField(),
				TextAlignment = UITextAlignment.Center,
				BorderStyle = UITextBorderStyle.RoundedRect
			};
			LocationPicker.Model = new LocationPickerModel(LocationField);

			DescriptionField.Layer.BorderColor = UIColor.Gray.CGColor;
			DescriptionField.Layer.CornerRadius = 5;
			DescriptionField.Layer.MasksToBounds = true;
			DescriptionField.Font = UIFont.FromName("SanFranciscoDisplay-Regular", 18);

			navController.NavigationBar.Translucent = false;

			View.BackgroundColor = UIColor.White;

			var parentView = View;

			View.AddSubviews(new UIView[] {
				TitleField,
				AudienceField,
				DescriptionField,
				StartTimeField,
				EndTimeField,
				LocationField,
				UpdateButton,
				DeleteButton
			});

			TitleField.MakeConstraints(make =>
			{
				make.Top.EqualTo(parentView).Offset(10);
				make.Width.EqualTo(parentView);
				make.Left.EqualTo(parentView).Offset(10);
				make.Height.EqualTo((NSNumber) 40);
			});

			AudienceField.MakeConstraints(make =>
			{
				make.Top.EqualTo(TitleField.Bottom()).Offset(10);
				make.Width.EqualTo(parentView);
				make.Left.EqualTo(TitleField);
				make.Height.EqualTo((NSNumber)40);
			}); 

			DescriptionField.MakeConstraints(make =>
			{
				make.Top.EqualTo(AudienceField.Bottom()).Offset(10);
				make.Width.EqualTo(TitleField);
				make.Left.EqualTo(TitleField);
				make.Height.EqualTo((NSNumber)80);
			});

			StartTimeField.MakeConstraints(make =>
			{
				make.Top.EqualTo(DescriptionField.Bottom()).Offset(10);
				make.Width.EqualTo(AudienceField);
				make.Left.EqualTo(TitleField);
				make.Height.EqualTo((NSNumber)30);
			});

			EndTimeField.MakeConstraints(make =>
			{
				make.Top.EqualTo(StartTimeField.Bottom()).Offset(10);
				make.Width.EqualTo(AudienceField);
				make.Left.EqualTo(TitleField);
				make.Height.EqualTo((NSNumber)30);
			});

			LocationField.MakeConstraints(make =>
			{
				make.Top.EqualTo(EndTimeField.Bottom()).Offset(10);
				make.Width.EqualTo(AudienceField);
				make.Left.EqualTo(TitleField);
				make.Height.EqualTo((NSNumber)30);
			});

			DeleteButton.MakeConstraints(make =>
			{
				make.Top.EqualTo(LocationField.Bottom()).Offset(10);
				make.Width.EqualTo(parentView).MultipliedBy(0.5f);
				make.Left.EqualTo(TitleField);
				make.Height.EqualTo((NSNumber)50);
			});
			DeleteButton.SetTitle("DELETE", UIControlState.Normal);

			UpdateButton.MakeConstraints(make =>
			{
				make.Top.EqualTo(DeleteButton);
				make.Width.EqualTo(parentView).MultipliedBy(0.5f);
				make.Right.EqualTo(parentView).Offset(-10);
				make.Height.EqualTo((NSNumber)50);
			});
			UpdateButton.SetTitle("SAVE", UIControlState.Normal);

			TitleField.Text = Event.Title;
			AudienceField.Text = Event.Audience;
			DescriptionField.Text = Event.Description;
			StartTimeField.Text = Utilities.FormatDateTime(Event.StartTime);
			EndTimeField.Text = Utilities.FormatDateTime(Event.EndTime);

			UpdateButton.TouchUpInside += async (sender, e) =>
			{
				Event updatedEvent = new Event()
				{
					Id = Event.Id,
					Title = TitleField.Text,
					Description = DescriptionField.Text,
					Audience = AudienceField.Text,
					StartTime = IOSHelpers.Convert(StartTimePicker.Date),
					EndTime = IOSHelpers.Convert(EndTImePicker.Date),
					LocationId = (int)LocationPicker.SelectedRowInComponent(0),
				};
				await presenter.Save(updatedEvent);
			};
		}

		public class LocationPickerModel : UIPickerViewModel
		{
			UITextField owner;

			public List<string> locations;

			public LocationPickerModel(UITextField owner)
			{
				this.owner = owner;
				locations = new List<string>();
			}

			public override nint GetComponentCount(UIPickerView pickerView)
			{
				return 1;
			}

			public override string GetTitle(UIPickerView pickerView, nint row, nint component)
			{
				if (locations == null || locations.Count == 0) return "";
				return locations[(int)row];
			}

			public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
			{
				return locations.Count;
			}

			public override void Selected(UIPickerView pickerView, nint row, nint component)
			{
				owner.Text = GetTitle(pickerView, row, component);
			}
		}

		public class AudiencePickerModel : UIPickerViewModel
		{
			UITextField owner;
			public AudiencePickerModel(UITextField owner)
			{
				this.owner = owner;
			}

			public override nint GetComponentCount(UIPickerView pickerView)
			{
				return 1;
			}

			public override string GetTitle(UIPickerView pickerView, nint row, nint component)
			{
				switch (row)
				{
					case 0: return "general";
					case 1: return "brothers";
					case 2: return "sisters";
				}
				return "";
			}

			public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
			{
				return 3;
			}

			public override void Selected(UIPickerView pickerView, nint row, nint component)
			{
				owner.Text = GetTitle(pickerView, row, component);
			}
		}

	}
}
