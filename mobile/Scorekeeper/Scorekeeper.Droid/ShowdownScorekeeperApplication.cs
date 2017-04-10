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

using Common.Common;
using Admin.Common.API;

namespace Scorekeeper.Droid
{
    [Application(
        Icon = "@drawable/ic_launcher", 
        Theme = "@android:style/Theme.Material")]
    class ShowdownScorekeeperApplication : Application
    {
        public AdminRESTClient BackendClient { get; set; }

        public ShowdownScorekeeperApplication(IntPtr handle, JniHandleOwnership ownership) : base(handle, ownership)
		{
            BackendClient = new AdminRESTClient();
        }

        public override void OnCreate()
        {
            base.OnCreate();
        }
    }
}