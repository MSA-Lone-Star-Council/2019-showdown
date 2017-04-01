using System;
using Common.iOS;
using Foundation;
using Masonry;
using UIKit;

namespace Admin.iOS
{
	public partial class GameEditorViewController
	{
		UITextField titleField = new UITextField()
		{
			BorderStyle = UITextBorderStyle.RoundedRect,
			TextAlignment = UITextAlignment.Center,
			Font = UIFont.SystemFontOfSize(18, UIFontWeight.Semibold),
			Placeholder = "Brief title, e.g. Elimination, Finals, etc"
		};

		UITextField eventField;
		UIPickerView eventPicker = new UIPickerView();

		UITextField scorekeeperField;
		UIPickerView scorekeeperPicker = new UIPickerView();

		UITextField awayTeamField;
		UIPickerView awayTeamPicker = new UIPickerView();

		UITextField homeTeamField;
		UIPickerView homeTeamPicker = new UIPickerView();

		UISwitch inProgressSwitch = new UISwitch();

		UIButton saveButton = new UIButton() { BackgroundColor = UIColor.Green };
		UIButton deleteButton = new UIButton() { BackgroundColor = UIColor.Red };

		partial void AdditionalSetup();

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
			var navController = appDelegate.Window.RootViewController as UINavigationController;
			navController.NavigationBar.Translucent = false;

			View.BackgroundColor = UIColor.White;

			UIToolbar keyboardToolbar = new UIToolbar();
			keyboardToolbar.SizeToFit();
			UIBarButtonItem flexibleSpace = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace, null);
			UIBarButtonItem doneBarButton = new UIBarButtonItem(UIBarButtonSystemItem.Done, (sender, e) => View.EndEditing(true));
			keyboardToolbar.Items = new UIBarButtonItem[] { flexibleSpace, doneBarButton };

			titleField.InputAccessoryView = keyboardToolbar;
			eventField = CreatePickerTextField(eventPicker, keyboardToolbar);

			var scorekeeperView = CreateScorekeeperView(keyboardToolbar);
			var teamsView = CreateTeamsView(keyboardToolbar);
			var progressView = CreateInProgressView();

			var parentView = View;

			parentView.AddSubviews(new UIView[] {
				titleField,
				eventField,
				scorekeeperView,
				teamsView,
				progressView,
				deleteButton,
				saveButton
			});

			titleField.MakeConstraints(make =>
			{
				make.Top.EqualTo(parentView).Offset(10);
				make.Width.EqualTo(parentView).MultipliedBy(0.95f);
				make.CenterX.Equals(parentView);
				make.Height.EqualTo((NSNumber)40);
			});
			eventField.MakeConstraints(make =>
			{
				make.Top.EqualTo(titleField.Bottom()).Offset(10);
				make.Width.EqualTo(titleField);
				make.CenterX.EqualTo(titleField);
				make.Height.EqualTo(titleField);
			});
			scorekeeperView.MakeConstraints(make =>
			{
				make.CenterX.EqualTo(titleField);
				make.Top.EqualTo(eventField.Bottom()).Offset(20);
				make.Width.EqualTo(parentView).MultipliedBy(0.95f);
				make.Height.EqualTo((NSNumber)80);
			});
			teamsView.MakeConstraints(make =>
			{
				make.CenterX.EqualTo(titleField);
				make.Top.EqualTo(scorekeeperView.Bottom()).Offset(10);
				make.Width.EqualTo(parentView).MultipliedBy(0.95f);
				make.Height.EqualTo((NSNumber)120);
			});
			progressView.MakeConstraints(make =>
			{
				make.CenterX.EqualTo(titleField);
				make.Top.EqualTo(teamsView.Bottom()).Offset(10);
				make.Width.EqualTo(parentView);
				make.Height.EqualTo((NSNumber)40);
			});

			deleteButton.MakeConstraints(make =>
			{
				make.Top.EqualTo(progressView.Bottom()).Offset(10);
				make.Width.EqualTo(parentView).MultipliedBy(0.5f);
				make.Left.EqualTo(progressView.Left());
				make.Height.EqualTo((NSNumber)50);
			});
			deleteButton.SetTitle("DELETE", UIControlState.Normal);

			saveButton.MakeConstraints(make =>
			{
				make.Top.EqualTo(deleteButton);
				make.Width.EqualTo(parentView).MultipliedBy(0.5f);
				make.Right.EqualTo(parentView).Offset(-10);
				make.Height.EqualTo((NSNumber)50);
			});
			saveButton.SetTitle("SAVE", UIControlState.Normal);

			AdditionalSetup();

		}

		private UITextField CreatePickerTextField(UIPickerView picker, UIToolbar keyboardToolbar)
		{
			return new UITextField()
			{
				InputView = picker,
				Delegate = new UneditableTextField(),
				TextAlignment = UITextAlignment.Center,
				BorderStyle = UITextBorderStyle.RoundedRect,
				InputAccessoryView = keyboardToolbar
			};
		}

		private UIView CreateScorekeeperView(UIToolbar keyboardToolbar)
		{
			UIView container = new UIView();
			scorekeeperField = CreatePickerTextField(scorekeeperPicker, keyboardToolbar);

			var scorekeeperLabel = new UILabel() { Text = "Scorekeeper", TextAlignment = UITextAlignment.Center };

			container.AddSubviews(new UIView[] { scorekeeperLabel, scorekeeperField });

			scorekeeperLabel.MakeConstraints(make =>
			{
				make.Top.EqualTo(container);
				make.Width.EqualTo(container);
				make.CenterX.EqualTo(container);
			});
			scorekeeperField.MakeConstraints(make =>
			{
				make.Width.EqualTo(container);
				make.CenterX.EqualTo(scorekeeperLabel);
				make.Top.EqualTo(scorekeeperLabel.Bottom()).Offset(5);
				make.Height.EqualTo((NSNumber)40);
			});

			return container;
		}

		private UIView CreateTeamsView(UIToolbar keyboardToolbar)
		{
			UIView container = new UIView();

			homeTeamField = CreatePickerTextField(homeTeamPicker, keyboardToolbar);
			awayTeamField = CreatePickerTextField(awayTeamPicker, keyboardToolbar);

			var vsLabel = new UILabel() { Text = "vs", TextAlignment = UITextAlignment.Center };

			container.AddSubviews(new UIView[] { awayTeamField, vsLabel, homeTeamField });

			awayTeamField.MakeConstraints(make =>
			{
				make.Top.EqualTo(container);
				make.CenterX.EqualTo(container);
				make.Width.EqualTo(container);
				make.Height.EqualTo((NSNumber)40);
			});

			vsLabel.MakeConstraints(make =>
			{
				make.Top.EqualTo(awayTeamField.Bottom()).Offset(5);
				make.CenterX.EqualTo(awayTeamField);
				make.Width.EqualTo(container);
			});

			homeTeamField.MakeConstraints(make =>
			{
				make.Top.EqualTo(vsLabel.Bottom()).Offset(5);
				make.Width.EqualTo(container);
				make.CenterX.EqualTo(awayTeamField);
				make.Height.EqualTo(awayTeamField);
			});

			return container;
		}

		private UIView CreateInProgressView()
		{
			UIView container = new UIView();
			var inprogressLabel = new UILabel() { Text = "In Progress", TextAlignment = UITextAlignment.Right };

			container.AddSubviews(new UIView[] { inprogressLabel, inProgressSwitch });

			inprogressLabel.MakeConstraints(make =>
			{
				make.Left.EqualTo(container);
				make.Width.EqualTo(container).MultipliedBy(0.5f);
				make.CenterY.EqualTo(container);
			});
			inProgressSwitch.MakeConstraints(make =>
			{
				make.Width.EqualTo(container).MultipliedBy(0.5f);
				make.CenterY.EqualTo(container);
				make.Height.EqualTo((NSNumber)40);
				make.Left.EqualTo(inprogressLabel.Right()).Offset(5);
			});

			return container;
		}

	}
}
