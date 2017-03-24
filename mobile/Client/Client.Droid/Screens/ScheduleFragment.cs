using Android.Support.V4.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using Client.Droid.Adapters;
using Common.Common;

namespace Client.Droid.Screens
{
    public class ScheduleFragment : Fragment
    {

        RecyclerView ScheduleView;


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_schedule, container, false);

            // Set up Recycler View for the Schedule
            ScheduleView = view.FindViewById<RecyclerView>(Resource.Id.scheduleRecyclerView);

            ScheduleAdapter adapter = new ScheduleAdapter(ShowdownRESTClient.MakeFakeData());
            ScheduleView.SetAdapter(adapter);
            ScheduleView.SetLayoutManager(new LinearLayoutManager(this.Activity));

            adapter.ItemClick += OnItemClick;

            return view;
        }

        // Handler for the item click event:
        void OnItemClick(object sender, ScheduleAdapterClickEventArgs args)
        {
            // Display a toast that briefly shows the enumeration of the selected photo:
            int itemNumber = args.Position + 1;
            Toast.MakeText(this.Activity, "This is event number " + itemNumber, ToastLength.Short).Show();
            StartActivity(new Intent(this.Activity, typeof(DetailedEventActivity)));
        }
    }
}

