using System;
using System.Collections.Generic;
using System.Linq;
using Android.Support.V4.App;
using Client.Droid.Screens;
using Common.Common.Models;
using Java.Lang;

namespace Client.Droid.Adapters
{
	class SchedulePagerAdapter : FragmentPagerAdapter
	{
		string[] titles = new string[] { "March 30", "March 31", "April 1" };
		public List<Event> Events { get; set; }

		public SchedulePagerAdapter(FragmentManager fm) : base(fm)
		{
		}

		public override int Count => 3;

		public override Fragment GetItem(int position)
		{
			ScheduleFragment fragment = new ScheduleFragment();
			var list = GetDayEvents(position);
			fragment.DayEvents = list;
			return fragment;
		}

		public override int GetItemPosition(Java.Lang.Object @object)
		{
            return PositionNone;
		}

		public override ICharSequence GetPageTitleFormatted(int position)
		{
			return new Java.Lang.String(titles[position]);
		}

		private List<Event> GetDayEvents(int day) {
			var eventsByDay = Events.GroupBy(x => x.StartTime.Day).ToArray();
			return eventsByDay[day].ToList();
		}
	}
}