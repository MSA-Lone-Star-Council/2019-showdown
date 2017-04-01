using System;
using UIKit;

namespace Common.iOS
{
	public class ConfirmActionAlert : UIAlertViewDelegate
	{
		public Action ConfirmAction { get; set; }
		
		public ConfirmActionAlert(Action confirm)
		{
			this.ConfirmAction = confirm;
		}

		public override void Clicked(UIAlertView alertview, nint buttonIndex)
		{
			switch (buttonIndex)
			{
				case 0: break;
				case 1: ConfirmAction(); break;
			}
			return;
		}
	}
}
