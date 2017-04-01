using System;
using Common.Common;
using Common.iOS;
using UIKit;

namespace Admin.iOS
{
	public class AdminAnnouncementsViewController : AnnoucementsViewController
	{
		public AdminAnnouncementsViewController(IAnnoucementInteractor client) : base(client)
		{
		}

		public async override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			ParentViewController.NavigationItem.RightBarButtonItem = new UIBarButtonItem(UIBarButtonSystemItem.Add, (sender, e) => Presenter.OnClickAdd());
		}

		public override void OpenNewAnnouncement()
		{
			var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
			var navController = appDelegate.Window.RootViewController as UINavigationController;
			navController.PushViewController(new AnnouncementEditorViewController(), true);
		}
	}
}
