using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Util;
using Android.Views;
using Android.Widget;
using Client.Common;
using Client.Droid.Adapters;
using Common.Common.Models;

namespace Client.Droid.Screens
{
	public class SchedulePagerFragment : Fragment, IScheduleView
	{
		ViewPager viewPager;
		TabLayout tabLayout;
		SchedulePagerAdapter pagerAdapter;

		List<Event> IScheduleView.Events
		{
			set
			{
				SchedulePagerAdapter adapter = this.pagerAdapter;
				if (adapter == null) return;
				adapter.Events = value;
				adapter.NotifyDataSetChanged();
			}
		}
		SchedulePresenter Presenter { get; set; }
		Timer timer = new Timer(TimeSpan.FromSeconds(10).TotalMilliseconds) { AutoReset = true };

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			Presenter = new SchedulePresenter(((ShowdownClientApplication)this.Activity.Application).BackendClient);
			Presenter.TakeView(this);
			pagerAdapter = new SchedulePagerAdapter(ChildFragmentManager)
			{
				Events = new List<Event>()
			};
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);
			var view = inflater.Inflate(Resource.Layout.fragment_schedule_pager, container, false);
			viewPager = view.FindViewById<ViewPager>(Resource.Id.view_pager);
			
			viewPager.Adapter = pagerAdapter;
			tabLayout = view.FindViewById<TabLayout>(Resource.Id.tabs);
			tabLayout.SetupWithViewPager(viewPager);

			return view;
		}

		public async override void OnResume()
		{
			base.OnResume();

			Presenter.TakeView(this);
			await Presenter.OnBegin();

			timer.Elapsed += (sender, e) => Activity.RunOnUiThread(async () => await Presenter.OnTick());
			timer.Start();
		}

		public override void OnStop()
		{
			base.OnStop();
			timer.Stop();
			Presenter.RemoveView();
		}

		public void OpenEvent(Event row)
		{
			return;
		}

		public Task ScheduleReminder(Event eventToRemind)
		{
			return null;
		}

		public void ShowMessage(string message)
		{
			return;
		}
	}
}