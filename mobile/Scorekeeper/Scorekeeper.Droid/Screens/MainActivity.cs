﻿using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Xamarin.Facebook;
using Xamarin.Facebook.Login.Widget;
using Xamarin.Facebook.Login;
using Java.Lang;
using System;

namespace Scorekeeper.Droid
{
    [Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, IFacebookCallback
    {
        private LoginButton loginButton;
        private ICallbackManager callbackManager;
        private MyProfileTracker profileTracker;

        private TextView name;
        private ProfilePictureView profilePicture;
        private Button continueButton;

        private string accessToken;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            FacebookSdk.SdkInitialize(this.ApplicationContext);
            callbackManager = CallbackManagerFactory.Create();

            profileTracker = new MyProfileTracker();
            profileTracker.mOnProfileChanged += OnProfileChanged;
            profileTracker.StartTracking();

            SetContentView(Resource.Layout.Main);

            loginButton = FindViewById<LoginButton>(Resource.Id.login_button);
            loginButton.SetReadPermissions("email");
            loginButton.RegisterCallback(callbackManager, this);

            name = FindViewById<TextView>(Resource.Id.facebook_name);
            profilePicture = FindViewById<ProfilePictureView>(Resource.Id.facebook_profile_picture);
            continueButton = FindViewById<Button>(Resource.Id.continue_button);
            continueButton.Click += delegate
            {
                var intent = new Intent(this, typeof(GameListActivity));
                intent.PutExtra("access_token", accessToken);
                StartActivity(intent);
            };
        }

        //Without this, the Facebook Callback mananger won't run
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            callbackManager.OnActivityResult(requestCode, (int)resultCode, data);
        }

        void IFacebookCallback.OnCancel()
        {
            throw new NotImplementedException();
        }

        void IFacebookCallback.OnError(FacebookException error)
        {
            throw new NotImplementedException();
        }

        void IFacebookCallback.OnSuccess(Java.Lang.Object result)
        {
            //throw new NotImplementedException();
            LoginResult loginResult = result as LoginResult;
            accessToken = loginResult.AccessToken.Token;

        }

        void OnProfileChanged(object sender, OnProfileChangedEventArgs e)
        {
            if (e.mProfile != null)
            {
                var profile = e.mProfile;
                name.Text = profile.Name;
                profilePicture.ProfileId = profile.Id;
                continueButton.Visibility = Android.Views.ViewStates.Visible;
            }
            else
            {
                name.Text = "Name";
                profilePicture.ProfileId = null;
                continueButton.Visibility = Android.Views.ViewStates.Gone;
            }
        }
    }

    public class MyProfileTracker : ProfileTracker
    {
        public event EventHandler<OnProfileChangedEventArgs> mOnProfileChanged;

        protected override void OnCurrentProfileChanged(Profile oldProfile, Profile newProfile)
        {
            if (mOnProfileChanged != null)
            {
                mOnProfileChanged.Invoke(this, new OnProfileChangedEventArgs(newProfile));
            }
        }
    }

    public class OnProfileChangedEventArgs : EventArgs
    {
        public Profile mProfile;

        public OnProfileChangedEventArgs(Profile profile) { mProfile = profile; }
    }
}

