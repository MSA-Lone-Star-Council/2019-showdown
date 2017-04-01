using System;
using Common.Common.Models;
using Masonry;
using UIKit;

namespace Client.iOS
{
	public class SchoolHeader : UIView
	{
		static UIFont SchoolTitleFont = UIFont.SystemFontOfSize(18, UIFontWeight.Bold);

		UILabel schoolTitleLabel = new UILabel() { Font = SchoolTitleFont };

		public SchoolHeader()
		{
			BackgroundColor = UIColor.White;
			Layer.BorderColor = UIColor.LightGray.CGColor;
			Layer.BorderWidth = 0.5f;

			AddSubview(schoolTitleLabel);

			schoolTitleLabel.MakeConstraints(make =>
			{
				make.Center.EqualTo(this);
			});
		}

		public School School
		{
			set
			{
				schoolTitleLabel.Text = value.Name;
			}
		}
	}
}
