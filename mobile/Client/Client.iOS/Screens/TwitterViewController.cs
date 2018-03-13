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
            webView = new UIWebView(View.Bounds);
            webView.ScalesPageToFit = true;
            View.AddSubview(webView);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
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

