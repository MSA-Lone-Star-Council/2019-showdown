using Android.Support.V4.App;
using Android.OS;
using Android.Views;
using Client.Common;
using Android.Content;
using Android.Net;
using Android.Widget;
using Android.Webkit;
using Common.Common;

namespace Client.Droid
{
	public class TwitterFragment : Fragment, ITweetView
	{
		TweetPresenter Presenter { get; set; }

		private TextView emptyTextView;
		private WebView webView;
		public WebView WebView
		{
			get
			{
				return webView;
			}
		}

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

            Presenter = new TweetPresenter(((ShowdownClientApplication)this.Activity.Application).BackendClient);
			Presenter.TakeView(this);
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View view = inflater.Inflate(Resource.Layout.fragment_twitter, container, false);
			emptyTextView = view.FindViewById<TextView>(Resource.Id.empty_view);
			webView = view.FindViewById<WebView>(Resource.Id.web_view);
			return view;
		}

		public override void OnResume()
		{
			base.OnResume();
			Presenter.TakeView(this);
			Presenter.OnBegin();
			webView.OnResume();
		}

		public override void OnPause()
		{
			base.OnPause();
			webView.OnPause();
		}

		public override void OnStop()
		{
			base.OnStop();
			Presenter.RemoveView();
		}

		void ITweetView.NoInternetConnection()
		{
			webView.Visibility = ViewStates.Gone;
			emptyTextView.Text = GetString(Resource.String.no_internet);
		}

		void ITweetView.StartWebView(string url)
		{
			if (webView.Visibility == ViewStates.Gone)
			{
				webView.Visibility = ViewStates.Visible;
				emptyTextView.Text = "";
				webView.Settings.JavaScriptEnabled = true;
				webView.SetWebViewClient(new TwitterWebViewClient());
				webView.LoadUrl(url);
			}
		}

		private class TwitterWebViewClient : WebViewClient
		{
			public override bool ShouldOverrideUrlLoading(WebView view, IWebResourceRequest request)
			{
				view.LoadUrl(request.Url.ToString());
				return false;
			}
		}
	}
}