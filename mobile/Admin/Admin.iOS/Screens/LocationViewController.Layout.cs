using System;
using Foundation;
using Masonry;
using UIKit;

namespace Admin.iOS
{
	public partial class LocationViewController
	{
		UITextField NameField = new UITextField()
		{
			Font = UIFont.FromName("SanFranciscoDisplay-Bold", 20),
			TextAlignment = UITextAlignment.Center,
			BorderStyle = UITextBorderStyle.RoundedRect
		};

		UITextField AddressField = new UITextField()
		{
			Font = UIFont.FromName("SanFranciscoDisplay-Bold", 16),
			TextAlignment = UITextAlignment.Center,
			BorderStyle = UITextBorderStyle.RoundedRect
		};

		UITextView NotesField = new UITextView();

		UIButton UpdateButton = new UIButton() { BackgroundColor = UIColor.Green };
		UIButton DeleteButton = new UIButton() { BackgroundColor = UIColor.Red };

		partial void AdditionalSetup();

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
			var navController = appDelegate.Window.RootViewController as UINavigationController;
			navController.NavigationBar.Translucent = false;

			UIToolbar keyboardToolbar = new UIToolbar();
			keyboardToolbar.SizeToFit();
			UIBarButtonItem flexibleSpace = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace, null);
			UIBarButtonItem doneBarButton = new UIBarButtonItem(UIBarButtonSystemItem.Done, (sender, e) => View.EndEditing(true));
			keyboardToolbar.Items = new UIBarButtonItem[] { flexibleSpace, doneBarButton };

			NameField.InputAccessoryView = keyboardToolbar;

			NotesField.Selectable = true;
			NotesField.Layer.BorderColor = UIColor.Gray.CGColor;
			NotesField.Layer.CornerRadius = 5;
			NotesField.Layer.MasksToBounds = true;
			NotesField.Font = UIFont.FromName("SanFranciscoDisplay-Regular", 18);
			NotesField.InputAccessoryView = keyboardToolbar;

			View.BackgroundColor = UIColor.White;

			var parentView = View;

			View.AddSubviews(new UIView[] {
				NameField,
				AddressField,
				NotesField,
				DeleteButton,
				UpdateButton
			});

			NameField.MakeConstraints(make =>
			{
				make.Top.EqualTo(parentView).Offset(10);
				make.Width.EqualTo(parentView);
				make.Left.EqualTo(parentView).Offset(10);
				make.Height.EqualTo((NSNumber)40);
			});

			AddressField.MakeConstraints(make =>
			{
				make.Top.EqualTo(NameField.Bottom()).Offset(10);
				make.Width.EqualTo(NameField);
				make.Left.EqualTo(NameField);
				make.Height.EqualTo((NSNumber)40);
			});

			NotesField.MakeConstraints(make =>
			{
				make.Top.EqualTo(AddressField.Bottom()).Offset(10);
				make.Width.EqualTo(NameField);
				make.Left.EqualTo(NameField);
				make.Height.EqualTo((NSNumber)80);
			});

			DeleteButton.MakeConstraints(make =>
			{
				make.Top.EqualTo(NotesField.Bottom()).Offset(10);
				make.Width.EqualTo(parentView).MultipliedBy(0.5f);
				make.Left.EqualTo(NameField);
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

			AdditionalSetup();
		}

	}
}
