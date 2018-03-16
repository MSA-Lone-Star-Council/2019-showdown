using Android.App;
using Android.OS;
using V4 = Android.Support.V4;
using Client.Droid.Screens;
using Android.Gms.Common;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Android.Support.V7.App;
using Common.Common;
using BottomNavigationBar;
using Android.Support.V4.Content;
using Android.Graphics;
using Android.Views;
using Android.Webkit;
using Android.Support.V7.Widget;

namespace Client.Droid
{
	[Activity(Label = "@string/app_name_short", MainLauncher = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait,
	WindowSoftInputMode = Android.Views.SoftInput.AdjustPan)]
	public class MainActivity : AppCompatActivity, BottomNavigationBar.Listeners.IOnTabClickListener
	{
		int prevPosition = 0;
		BottomBar bottomBar;
		V4.App.Fragment[] Fragments { get; set; }

		public static string ScreenIndexKey = "ScreenIndex";

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.Main);
			AppCenter.Start(Secrets.clientAndroidAppCenterSecret, typeof(Analytics), typeof(Crashes));

			Fragments = new V4.App.Fragment[] {
			new SchedulePagerFragment(),
			new AnnouncementsFragment(),
			new TwitterFragment(),
			new InfoFragment()
			};
			bottomBar = BottomBar.Attach(
						this,
						savedInstanceState,
						new Color(255, 255, 255), // BG value as white
						new Color(ContextCompat.GetColor(this, Resource.Color.primaryColor)), // Active tab value as burnt orange
						0.4f);  // Transparency value

			bottomBar.SetItems(new[] {
				new BottomBarTab(Resource.Drawable.ic_event_white_24dp, Resource.String.schedule_title),
				new BottomBarTab(Resource.Drawable.ic_notifications_white_24dp, Resource.String.announcements_title),
				new BottomBarTab(Resource.Drawable.ic_twitter_white_24dp, Resource.String.twitter_title),
				new BottomBarTab(Resource.Drawable.ic_info_white_24dp, Resource.String.info_title)
			});
			bottomBar.SetOnTabClickListener(this);

			IsPlayServicesAvailable();

		}

		protected override void OnSaveInstanceState(Bundle outState)
		{
			base.OnSaveInstanceState(outState);
			bottomBar.OnSaveInstanceState(outState);
		}

		void IsPlayServicesAvailable()
		{
			int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
			if (resultCode != ConnectionResult.Success)
			{
				// TODO: If user solvable error, provide some helpful resolution
				Finish();
			}
		}

		public void OnTabSelected(int position)
		{
			V4.App.Fragment fragment = Fragments[position];
			V4.App.Fragment oldFragment = Fragments[prevPosition];

			if (SupportFragmentManager.FindFragmentByTag(prevPosition.ToString()) != null)
			{
				SupportFragmentManager.BeginTransaction()
				.Hide(oldFragment)
				.Commit();
			}
			if (SupportFragmentManager.FindFragmentByTag(position.ToString()) == null)
			{
				SupportFragmentManager.BeginTransaction()
				.Add(Resource.Id.content_frame, fragment, position.ToString())
				.Commit();
			}
			else
			{
				SupportFragmentManager.BeginTransaction()
				.Show(fragment)
				.Commit();
			}
			prevPosition = position;
		}

		public void OnTabReSelected(int position)
		{
			RecyclerView scrollingView = null;
			switch (position)
			{
				case 0:
					SchedulePagerFragment schedulePagerFragment = (SchedulePagerFragment)Fragments[position];
					int currentItem = schedulePagerFragment.ViewPager.CurrentItem;
					ScheduleFragment scheduleFragment =
						(ScheduleFragment)schedulePagerFragment.ChildFragmentManager
						.FindFragmentByTag("android:switcher:" + Resource.Id.view_pager + ":" + currentItem);
					scrollingView = scheduleFragment.ScheduleList;
					break;
				case 1:
					scrollingView = (Fragments[position] as AnnouncementsFragment).AnnouncementList;
					break;
				case 2:
					WebView webView = (Fragments[position] as TwitterFragment).WebView;
					if (webView != null)
					{
						webView.ScrollTo(0, 0);
					}
					break;
			}
			if (scrollingView != null)
			{
				scrollingView.GetLayoutManager().ScrollToPosition(0);
			}
		}
	}
}