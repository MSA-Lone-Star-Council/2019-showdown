using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;
using Android.Text;

namespace Client.Droid.Screens
{
    public class AcknowledgementsFragment : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            var view = inflater.Inflate(Resource.Layout.fragment_acknowledgements, container, false);

            TextView tv = view.FindViewById<TextView>(Resource.Id.lsc_link);
            tv.Clickable = true;
            tv.Click += delegate
            {
                var uri = Android.Net.Uri.Parse("https://www.msa-texas.org/");
                var intent = new Intent(Intent.ActionView, uri);
                StartActivity(intent);
            };

            ImageView twitter = view.FindViewById<ImageView>(Resource.Id.twitter_pic);
            twitter.Clickable = true;
            twitter.Click += delegate
            {
                var uri = Android.Net.Uri.Parse("https://twitter.com/msalsc");
                var intent = new Intent(Intent.ActionView, uri);
                StartActivity(intent);
            };

            ImageView facebook = view.FindViewById<ImageView>(Resource.Id.fb_pic);
            facebook.Clickable = true;
            facebook.Click += delegate
            {
                var uri = Android.Net.Uri.Parse("https://www.facebook.com/msalonestarcouncil/");
                var intent = new Intent(Intent.ActionView, uri);
                StartActivity(intent);
            };

            TextView gh = view.FindViewById<TextView>(Resource.Id.github_link);
            gh.Clickable = true;
            gh.Click += delegate
            {
                var uri = Android.Net.Uri.Parse("https://github.com/MSA-Lone-Star-Council/Showdown");
                var intent = new Intent(Intent.ActionView, uri);
                StartActivity(intent);
            };

            return view;
        }
    }
}