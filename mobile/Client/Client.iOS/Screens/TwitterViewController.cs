using System;
using Client.Common;
using Foundation;
using UIKit;

namespace Client.iOS
{
    public class TwitterViewController : UIViewController, ITweetView
    {
        TweetPresenter presenter;
        UIWebView webView;

        public TwitterViewController() : base("TwitterViewController", null)
        {
            var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
            presenter = new TweetPresenter(appDelegate.BackendClient);
            View.AutosizesSubviews = true;
            this.Title = "#TxShowdown18";
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib
            webView = new UIWebView(UIScreen.MainScreen.Bounds);

            webView.ScalesPageToFit = true;
            View.AddSubview(webView);
            this.ViewDidLayoutSubviews();

            presenter.TakeView(this);

        }

		public override void ViewDidAppear(bool animated)
		{
            base.ViewDidAppear(animated);
            presenter.OnBegin();
		}


		void ITweetView.StartWebView(string url)
        {
            webView.LoadRequest(new NSUrlRequest(new NSUrl(url)));
        }

        void ITweetView.NoInternetConnection()
        {
            //Create Alert
            var okAlertController = UIAlertController.Create("Error", "Not connected to the internet", UIAlertControllerStyle.Alert);
            //Add Action
            okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
            // Present Alert
            PresentViewController(okAlertController, true, null);
        }
    }
}

