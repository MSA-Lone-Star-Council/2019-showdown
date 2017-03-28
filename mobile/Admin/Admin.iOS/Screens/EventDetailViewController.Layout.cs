using System;
using Common.Common;
using Common.iOS;
using Foundation;
using Masonry;
using UIKit;

namespace Admin.iOS
{
	public partial class EventDetailViewController
	{
		UITextField TitleField = new UITextField()
		{
			Font = UIFont.FromName("SanFranciscoDisplay-Bold", 20),
			TextAlignment = UITextAlignment.Center,
			BorderStyle = UITextBorderStyle.RoundedRect
		};

		UIPickerView AudiencePicker = new UIPickerView();
		UITextField AudienceField;

		UITextView DescriptionField = new UITextView();

		UIDatePicker StartTimePicker = new UIDatePicker() { TimeZone = new NSTimeZone("America/Chicago") };
		UITextField StartTimeField;

		UIDatePicker EndTimePicker = new UIDatePicker() { TimeZone = new NSTimeZone("America/Chicago") };
		UITextField EndTimeField;

		UIPickerView LocationPicker = new UIPickerView();
		UITextField LocationField;

		UIButton UpdateButton = new UIButton() { BackgroundColor = UIColor.Green };
		UIButton DeleteButton = new UIButton() { BackgroundColor = UIColor.Red };

		partial void AdditonalSetup();

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


			StartTimeField = new UITextField()
			{
				InputView = StartTimePicker,
				Delegate = new UneditableTextField(),
				TextAlignment = UITextAlignment.Center,
				BorderStyle = UITextBorderStyle.RoundedRect,
			};
			StartTimePicker.ValueChanged += (sender, e) =>
			{
				UIDatePicker picker = sender as UIDatePicker;
				StartTimeField.Text = Utilities.FormatEventTime(IOSHelpers.Convert(picker.Date));
			};

			EndTimeField = new UITextField()
			{
				InputView = EndTimePicker,
				Delegate = new UneditableTextField(),
				TextAlignment = UITextAlignment.Center,
				BorderStyle = UITextBorderStyle.RoundedRect
			};
			EndTimePicker.ValueChanged += (sender, e) =>
			{
				UIDatePicker picker = sender as UIDatePicker;
				EndTimeField.Text = Utilities.FormatEventTime(IOSHelpers.Convert(picker.Date));
			};

			AudienceField = new UITextField()
			{
				InputView = AudiencePicker,
				Delegate = new UneditableTextField(),
				TextAlignment = UITextAlignment.Center,
				BorderStyle = UITextBorderStyle.RoundedRect
			};


			LocationField = new UITextField()
			{
				InputView = LocationPicker,
				Delegate = new UneditableTextField(),
				TextAlignment = UITextAlignment.Center,
				BorderStyle = UITextBorderStyle.RoundedRect
			};


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
				make.Height.EqualTo((NSNumber)40);
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

			AdditonalSetup();
		}
	}
}
