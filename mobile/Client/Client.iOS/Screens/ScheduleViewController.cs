using System;

using UIKit;

namespace Client.iOS
{
	public class ScheduleViewController : UIViewController
	{
		public ScheduleViewController()
		{
			View.BackgroundColor = UIColor.Red;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

