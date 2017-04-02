using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace Scorekeeper.Droid
{
    [Activity(Label = "Scorekeeper.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        Button fakeLoginButton; 

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            fakeLoginButton = FindViewById<Button>(Resource.Id.fakeLoginButton);
            fakeLoginButton.Click += delegate
            {
                var intent = new Intent(this, typeof(GameListActivity));
                StartActivity(intent);
            };
        }
    }
}

