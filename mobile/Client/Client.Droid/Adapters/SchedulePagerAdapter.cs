using System;
using Android.Support.V4.App;
using Client.Droid.Screens;
using Java.Lang;

namespace Client.Droid.Adapters
{
	class SchedulePagerAdapter : FragmentPagerAdapter
	{
		string[] titles = new string[] { "March 30", "March 31", "April 1" };

		public SchedulePagerAdapter(FragmentManager fm) : base(fm)
		{
		}

		public override int Count => 3;

		public override Fragment GetItem(int position)
		{
			return new ScheduleFragment(position);
		}

		public override ICharSequence GetPageTitleFormatted(int position)
		{
			return new Java.Lang.String(titles[position]);
		}
	}
}