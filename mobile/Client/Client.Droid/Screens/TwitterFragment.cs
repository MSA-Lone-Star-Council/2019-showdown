using Android.Support.V4.App;
using Android.OS;
using Android.Views;
using Client.Common;
using Android.Content;
using Android.Net;
using Android.Widget;
using Android.Webkit;

namespace Client.Droid
{
	public class TwitterFragment : Fragment, ITweetView
	{
		TweetPresenter Presenter { get; set; }

		private TextView emptyTextView;
		private ProgressBar progressBar;
		private WebView webView;

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			Presenter = new TweetPresenter();
			Presenter.TakeView(this);
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View view = inflater.Inflate(Resource.Layout.fragment_twitter, container, false);
			emptyTextView = view.FindViewById<TextView>(Resource.Id.empty_view);
			progressBar = view.FindViewById<ProgressBar>(Resource.Id.progress_spinner);
			webView = view.FindViewById<WebView>(Resource.Id.web_view);
			Presenter.OnBegin();
			return view;
		}

		public override void OnResume()
		{
			base.OnResume();
			Presenter.TakeView(this);
		}

		public override void OnStop()
		{
			base.OnStop();
			Presenter.RemoveView();
		}

		bool ITweetView.HasInternetConnection()
		{
			ConnectivityManager connectivityManager = (ConnectivityManager)Context.GetSystemService(Context.ConnectivityService);
			NetworkInfo networkInfo = connectivityManager.ActiveNetworkInfo;
			bool isConnected = networkInfo != null && networkInfo.IsConnectedOrConnecting;
			return isConnected;
		}

		void ITweetView.NoInternetConnection()
		{
			webView.Visibility = ViewStates.Gone;
			progressBar.Visibility = ViewStates.Gone;
			emptyTextView.Text = GetString(Resource.String.no_internet);
		}

		void ITweetView.StartWebView(string url)
		{
			progressBar.Visibility = ViewStates.Gone;
			webView.Visibility = ViewStates.Visible;
			webView.Settings.JavaScriptEnabled = true;
			webView.SetWebViewClient(new TwitterWebViewClient());
			webView.LoadUrl(url);
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