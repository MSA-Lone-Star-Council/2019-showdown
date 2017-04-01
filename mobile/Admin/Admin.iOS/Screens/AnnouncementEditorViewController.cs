using System;
using Admin.Common;
using Common.Common.Models;
using UIKit;

namespace Admin.iOS
{
	public partial class AnnouncementEditorViewController : UIViewController, IAnnouncementEditorView
	{
		AnnouncementEditorPresenter Presenter;

		public AnnouncementEditorViewController()
		{
			var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
			Presenter = new AnnouncementEditorPresenter(appDelegate.BackendClient);
		}

		partial void AdditonalSetup()
		{
			SaveButton.TouchUpInside += async (sender, e) => await Presenter.Save();
		}

		void IAnnouncementEditorView.GoBack()
		{
			NavigationController.PopViewController(true);
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			Presenter.TakeView(this);

			TitleField.Placeholder = "Announcement Title - Keep it short";
			BodyField.Text = "Announcement body";
		}

		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);
			Presenter.RemoveView();
		}

		Announcement IAnnouncementEditorView.Announcement
		{
			get
			{
				return new Announcement()
				{
					Title = TitleField.Text,
					Body = BodyField.Text
				};
			}
		}
	}
}
