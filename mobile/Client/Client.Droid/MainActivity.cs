using Android.App;
using Android.Widget;
using Android.OS;
using V4 = Android.Support.V4;
using V7 = Android.Support.V7;
using Android.Support.V4.Widget;
using Android.Views;
using System;
using Client.Droid.Screens;
using Android.Gms.Common;
using Android.Util;
using Firebase.Iid;

namespace Client.Droid
{
    [Activity(Label = "Client.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : V4.App.FragmentActivity
    {
		public static string ScreenIndexKey = "ScreenIndex";

        string[] NavigationTitles { get; set; }
        DrawerLayout DrawerLayout { get; set; }
        V7.App.ActionBarDrawerToggle DrawerToggle { get; set; }
        ListView DrawerList { get; set; }

        V4.App.Fragment[] Fragments { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

			BuildDrawer();

            Fragments = new V4.App.Fragment[] {
                new ScheduleFragment(),
                new AnnouncementsFragment(),
                new SportsFragment(),
                new AcknowledgementsFragment()
            };


			IsPlayServicesAvailable();
        }

		protected override void OnResume()
		{
			base.OnResume();
			int screenIndex = Intent.GetIntExtra(ScreenIndexKey, 0);
			SelectItem(screenIndex);
		}
	

		void BuildDrawer()
		{
			ActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_drawer);
			ActionBar.SetDisplayHomeAsUpEnabled(true);
			ActionBar.SetHomeButtonEnabled(true);
			//ActionBar.SetDisplayShowTitleEnabled(false);

			NavigationTitles = Resources.GetStringArray(Resource.Array.nav_titles);

			DrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

			DrawerList = FindViewById<ListView>(Resource.Id.left_drawer);
			DrawerList.Adapter = new ArrayAdapter<string>(this, Resource.Layout.drawer_list_item, NavigationTitles);
			DrawerList.ItemClick += (sender, e) => SelectItem(e.Position);

			DrawerToggle = new V7.App.ActionBarDrawerToggle(
				this,
				DrawerLayout,
				Resource.String.drawer_open,
				Resource.String.drawer_close);
		}

        void SelectItem(int position)
        {
            var fragmentToShow = Fragments[position];
            this.SupportFragmentManager.BeginTransaction()
                                  .Replace(Resource.Id.content_frame, fragmentToShow)
                                  .Commit();

            DrawerList.SetItemChecked(position, true);
            this.Title = NavigationTitles[position];
            DrawerLayout.CloseDrawer(DrawerList);
        }


        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (DrawerToggle.OnOptionsItemSelected(item)) return true; // This gets the menu icon to work
            return base.OnOptionsItemSelected(item);
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
    }
}