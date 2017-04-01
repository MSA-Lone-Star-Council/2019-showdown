using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace Scorekeeper.Droid
{
    [Activity(Label = "Scorekeeper.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        Button tempSportsButton; 

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            tempSportsButton = FindViewById<Button>(Resource.Id.tempSportsButton);
            tempSportsButton.Click += delegate
            {
                var intent = new Intent(this, typeof(ScoreCardActivity));
                StartActivity(intent);
            };
        }
    }
}

