using System;
using Foundation;
using Masonry;
using UIKit;

namespace Admin.iOS
{
	public partial class AnnouncementEditorViewController
	{
		UITextField TitleField = new UITextField()
		{
			Font = UIFont.FromName("SanFranciscoDisplay-Bold", 20),
			TextAlignment = UITextAlignment.Center,
			BorderStyle = UITextBorderStyle.RoundedRect
		};

		UITextView BodyField = new UITextView();

		UIButton SaveButton = new UIButton() { BackgroundColor = UIColor.Green };

		partial void AdditonalSetup();

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			UIToolbar keyboardToolbar = new UIToolbar();
			keyboardToolbar.SizeToFit();
			UIBarButtonItem flexibleSpace = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace, null);
			UIBarButtonItem doneBarButton = new UIBarButtonItem(UIBarButtonSystemItem.Done, (sender, e) => View.EndEditing(true));
			keyboardToolbar.Items = new UIBarButtonItem[] { flexibleSpace, doneBarButton };

			TitleField.InputAccessoryView = keyboardToolbar;

			BodyField.Selectable = true;
			BodyField.Layer.BorderColor = UIColor.Gray.CGColor;
			BodyField.Layer.CornerRadius = 5;
			//BodyField.Layer.MasksToBounds = true;
			BodyField.Font = UIFont.FromName("SanFranciscoDisplay-Regular", 18);
			BodyField.InputAccessoryView = keyboardToolbar;

			var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
			var navController = appDelegate.Window.RootViewController as UINavigationController;
			navController.NavigationBar.Translucent = false;

			View.BackgroundColor = UIColor.White;

			var parentView = View;

			View.AddSubviews(new UIView[] {
				TitleField,
				BodyField,
				SaveButton
			});

			TitleField.MakeConstraints(make =>
			{
				make.Top.EqualTo(parentView).Offset(10);
				make.Width.EqualTo(parentView);
				make.Left.EqualTo(parentView).Offset(10);
				make.Height.EqualTo((NSNumber)40);
			});

			BodyField.MakeConstraints(make =>
			{
				make.Top.EqualTo(TitleField.Bottom()).Offset(10);
				make.Width.EqualTo(TitleField);
				make.Left.EqualTo(TitleField);
				make.Height.EqualTo((NSNumber)80);
			});

			SaveButton.MakeConstraints(make =>
			{
				make.Top.EqualTo(BodyField.Bottom()).Offset(10);
				make.Width.EqualTo(parentView).MultipliedBy(0.5f);
				make.CenterX.EqualTo(parentView);
				make.Height.EqualTo((NSNumber)50);
			});
			SaveButton.SetTitle("PUBLISH", UIControlState.Normal);

			AdditonalSetup();
		}
	}
}
