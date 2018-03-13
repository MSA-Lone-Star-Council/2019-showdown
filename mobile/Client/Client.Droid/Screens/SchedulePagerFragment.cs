using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Util;
using Android.Views;
using Android.Widget;
using Client.Droid.Adapters;

namespace Client.Droid.Screens
{
	public class SchedulePagerFragment : Fragment
	{
		ViewPager viewPager;
		TabLayout tabLayout;
		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);
			var view = inflater.Inflate(Resource.Layout.fragment_schedule_pager, container, false);
			viewPager = view.FindViewById<ViewPager>(Resource.Id.view_pager);
			SchedulePagerAdapter pagerAdapter = new SchedulePagerAdapter(ChildFragmentManager);
			viewPager.Adapter = pagerAdapter;
			tabLayout = view.FindViewById<TabLayout>(Resource.Id.tabs);
			tabLayout.SetupWithViewPager(viewPager);

			return view;
		}
	}
}