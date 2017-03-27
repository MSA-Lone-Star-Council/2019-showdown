using System;

using UIKit;
using Masonry;
using WebKit;
using Foundation;
using Common.Common;

namespace Common.iOS
{
	public delegate void OnAccessTokenEventHandler(string accessToken);

	public class FacebookLoginViewController : UIViewController
	{
		public event OnAccessTokenEventHandler OnAccessToken;

		private WKWebView webView;

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			NavigationItem.HidesBackButton = true;

			webView = new WKWebView(View.Bounds, new WKWebViewConfiguration());
			webView.NavigationDelegate = new NavigationDelegate(this);

			View.AddSubview(webView);

			webView.MakeConstraints(make =>
			{
				make.Size.EqualTo(View);
				make.Center.EqualTo(View);
			});

			var url = string.Format("https://www.facebook.com/v2.8/dialog/oauth?client_id={0}&redirect_uri={1}&response_type=token",
											 Secrets.FacebookAppId,
											 "https://www.facebook.com/connect/login_success.html");
			webView.LoadRequest(new NSUrlRequest(new NSUrl(url)));

		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		private class NavigationDelegate : WKNavigationDelegate
		{
			public FacebookLoginViewController controller;
			public NavigationDelegate(FacebookLoginViewController controller)
			{
				this.controller = controller;
			}

			public override void DecidePolicy(WKWebView webView, WKNavigationAction navigationAction, Action<WKNavigationActionPolicy> decisionHandler)
			{
				System.Diagnostics.Debug.WriteLine(navigationAction.Request.Url);
				if (navigationAction.Request.Url.Path != "/connect/login_success.html")
				{
					decisionHandler(WKNavigationActionPolicy.Allow);
					return;
				}
				decisionHandler(WKNavigationActionPolicy.Cancel);

				// Extract code from query
				string regexPattern = "access_token=(.*)&expires_in";
				var match = System.Text.RegularExpressions.Regex.Match(navigationAction.Request.Url.AbsoluteString, regexPattern);
				var accessCode = match.Groups[1].Value;

				var plist = NSUserDefaults.StandardUserDefaults;
				plist.SetString(accessCode, "FACEBOOK_TOKEN");
				plist.Synchronize();

				controller.NavigationController.PopViewController(false);
			}

		}
	}
}