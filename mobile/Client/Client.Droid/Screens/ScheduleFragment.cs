<<<<<<< HEAD
=======
﻿
>>>>>>> master
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

<<<<<<< HEAD
using Android.Support.V4.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
=======

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
>>>>>>> master
using Android.Util;
using Android.Views;
using Android.Widget;

<<<<<<< HEAD
namespace Client.Droid.Screens
{
    public class ScheduleFragment : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            return inflater.Inflate(Resource.Layout.fragment_schedule, container, false);
        }
    }
}
=======
namespace Client.Droid
{
	public class ScheduleFragment : Fragment
	{
		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			return inflater.Inflate(Resource.Layout.fragment_schedule, container, false);
		}
	}
}
>>>>>>> master
