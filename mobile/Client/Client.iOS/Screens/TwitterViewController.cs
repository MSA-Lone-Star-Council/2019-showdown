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
            presenter = new TweetPresenter();
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
            throw new NotImplementedException();
        }

        bool ITweetView.HasInternetConnection()
        {
            //throw new NotImplementedException();
            return true;
        }
    }
}

