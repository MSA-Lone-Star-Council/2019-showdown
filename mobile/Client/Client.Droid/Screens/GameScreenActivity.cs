using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Client.Common;
using Common.Common;
using Common.Common.Models;

namespace Client.Droid.Screens
{
    [Activity(Label = "GameScreenActivity")]
    public class GameScreenActivity : Activity, IGameView
    {
        public Game Game => throw new NotImplementedException();

        public List<Score> ScoreHistory { set => throw new NotImplementedException(); }

        public void ShowMessage(string message)
        {
            throw new NotImplementedException();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
        }
    }
}