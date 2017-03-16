using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Views;
using System;

namespace Client.Droid
{
	[Activity(Label = "Client.Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : FragmentActivity
	{

		string[] NavigationTitles { get; set; }
		DrawerLayout DrawerLayout { get; set; }
		Android.Support.V7.App.ActionBarDrawerToggle DrawerToggle { get; set; }
		ListView DrawerList { get; set; }

		Android.Support.V4.App.Fragment[] Fragments { get; set; }

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.Main);

			//ActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_drawer);
			ActionBar.SetDisplayHomeAsUpEnabled(true);
			ActionBar.SetHomeButtonEnabled(true);
			//ActionBar.SetDisplayShowTitleEnabled(false);

			NavigationTitles = Resources.GetStringArray(Resource.Array.nav_titles);

			DrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

			DrawerList = FindViewById<ListView>(Resource.Id.left_drawer);
			DrawerList.Adapter = new ArrayAdapter<string>(this, Resource.Layout.drawer_list_item, NavigationTitles);
			DrawerList.ItemClick += (sender, e) => SelectItem(e.Position);

			DrawerToggle = new Android.Support.V7.App.ActionBarDrawerToggle(
				this,
				DrawerLayout,
				Resource.String.drawer_open,
				Resource.String.drawer_close);

			Fragments = new Android.Support.V4.App.Fragment[] {
				new ScheduleFragment(),
				new AnnoucmentsFragment(),
				new SportsFragment(),
				new AcknowledgementsFragment()
			};

			if (savedInstanceState == null)
			{
				SelectItem(0);
			}

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
	}
}

