using System;
using Android.App;
using Android.Runtime;
using Common.Common;

namespace Client.Droid
{
    [Application(
        Icon = "@drawable/lsc_icon",
        Theme = "@android:style/Theme.Material")]
    public class ShowdownClientApplication : Application
	{
		public ShowdownRESTClient BackendClient { get; set; }

		public ShowdownClientApplication(IntPtr handle, JniHandleOwnership ownership) : base(handle, ownership)
		{
			BackendClient = new ShowdownRESTClient();
		}

		public override void OnCreate()
		{
			base.OnCreate();
		}
	}
}
