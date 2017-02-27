using System;

using UIKit;
using Masonry;
using Common.Common;
using Common.iOS;

namespace Scorekeeper.iOS
{
	public class LoginViewController : AbstractLoginViewController
	{
		public override void Advance()
		{
			// TODO: Go to an actual view controller
			var controller = new UIViewController();
			controller.View.BackgroundColor = UIColor.Green;
			controller.NavigationItem.HidesBackButton = true;
			NavigationController.PushViewController(controller, true);
		}
	}
}
