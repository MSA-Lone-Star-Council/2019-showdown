using System;
using Foundation;
using Masonry;
using UIKit;

namespace Client.iOS
{
    public class InfoViewController : UIViewController
    {

        UILabel ShowdownQuestion = new UILabel()
        {
            Text = "What is Showdown?",
            Font = UIFont.SystemFontOfSize(20, UIFontWeight.Bold),
            TextAlignment = UITextAlignment.Center
        };

        UILabel ShowdownAnswer = new UILabel()
        {
            Text = "Showdown is a three-day, tournament style conference which strives to deliever a platform for MSAs in Texas to network in a competitve setting.",
            Font = UIFont.SystemFontOfSize(16, UIFontWeight.Regular),
            TextAlignment = UITextAlignment.Center,
            Lines = 0,
        };

        UILabel LSCQuestion = new UILabel()
        {
            Text = "What is Lone Star Council?",
            Font = UIFont.SystemFontOfSize(20, UIFontWeight.Bold),
            TextAlignment = UITextAlignment.Center
        };

        UILabel LSCAnswer = new UILabel()
        {
            Text = "The MSA Lone Star Council is a coalition of MSAs across the state of Texas whose primary purpose is to unify MSAs for the sake of Allah",
            Font = UIFont.SystemFontOfSize(16, UIFontWeight.Regular),
            TextAlignment = UITextAlignment.Center,
            Lines = 0,
        };

        UILabel AppQuestion = new UILabel()
        {
            Text = "Who made the app?",
            Font = UIFont.SystemFontOfSize(20, UIFontWeight.Bold),
            TextAlignment = UITextAlignment.Center
        };

        UILabel AppAnswer = new UILabel()
        {
            Text = "A joint effort between Saad Najmi, Zuhair Parvez, Ali Naqvi, Hamza Khatri, Ali Kedwaii, and Adil Moosani",
            Font = UIFont.SystemFontOfSize(16, UIFontWeight.Regular),
            TextAlignment = UITextAlignment.Center,
            Lines = 0,
        };

        UIButton visitLink = new UIButton();

        UIButton twitterButton = new UIButton();
        UIButton facebookButton = new UIButton();

        UIButton contributionLink = new UIButton() { Font = UIFont.SystemFontOfSize(10, UIFontWeight.Bold) };

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.BackgroundColor = Resources.Colors.backgroundColor;
            this.Title = "MSA Showdown";

            View.AddSubviews(new UIView[]
            {
                ShowdownQuestion,
                ShowdownAnswer,
                LSCQuestion,
                LSCAnswer,
                AppQuestion,
                AppAnswer,
                visitLink,
                twitterButton,
                facebookButton,
                contributionLink
            });

            ShowdownQuestion.MakeConstraints(make =>
            {
                make.Top.EqualTo(View.Top()).Offset(10);
                make.CenterX.EqualTo(View.CenterX());
                make.Left.EqualTo(View.Left()).Offset(10);
                make.Right.EqualTo(View.Right()).Offset(-10);
            });

            ShowdownAnswer.MakeConstraints(make =>
            {
                make.Top.EqualTo(ShowdownQuestion.Bottom()).Offset(5);
                make.CenterX.EqualTo(View.CenterX());
                make.Left.EqualTo(View.Left()).Offset(10);
                make.Right.EqualTo(View.Right()).Offset(-10);
            });

            LSCQuestion.MakeConstraints(make =>
            {
                make.Top.EqualTo(ShowdownAnswer.Bottom()).Offset(10);
                make.CenterX.EqualTo(View.CenterX());
                make.Left.EqualTo(View.Left()).Offset(10);
                make.Right.EqualTo(View.Right()).Offset(-10);
            });

            LSCAnswer.MakeConstraints(make =>
            {
                make.Top.EqualTo(LSCQuestion.Bottom()).Offset(5);
                make.CenterX.EqualTo(View.CenterX());
                make.Left.EqualTo(View.Left()).Offset(10);
                make.Right.EqualTo(View.Right()).Offset(-10);
            });

            AppQuestion.MakeConstraints(make =>
            {
                make.Top.EqualTo(LSCAnswer.Bottom()).Offset(10);
                make.CenterX.EqualTo(View.CenterX());
                make.Left.EqualTo(View.Left()).Offset(10);
                make.Right.EqualTo(View.Right()).Offset(-10);
            });

            AppAnswer.MakeConstraints(make =>
            {
                make.Top.EqualTo(AppQuestion.Bottom()).Offset(5);
                make.CenterX.EqualTo(View.CenterX());
                make.Left.EqualTo(View.Left()).Offset(10);
                make.Right.EqualTo(View.Right()).Offset(-10);
            });

            visitLink.MakeConstraints(make =>
            {
                make.Top.EqualTo(AppAnswer.Bottom()).Offset(10);
                make.CenterX.EqualTo(View.CenterX());
                make.Left.EqualTo(View.Left()).Offset(10);
                make.Right.EqualTo(View.Right()).Offset(-10);
            });

            facebookButton.MakeConstraints(make =>
            {
                make.Top.EqualTo(visitLink.Bottom()).Offset(10);
                make.CenterX.EqualTo(View.CenterX());
                make.Left.EqualTo(View.Left()).Offset(10);
                make.Right.EqualTo(View.Right()).Offset(-10);
            });

            twitterButton.MakeConstraints(make =>
            {
                make.Top.EqualTo(facebookButton.Bottom()).Offset(5);
                make.CenterX.EqualTo(View.CenterX());
                make.Left.EqualTo(View.Left()).Offset(10);
                make.Right.EqualTo(View.Right()).Offset(-10);
            });

            contributionLink.MakeConstraints(make =>
            {
                make.Top.EqualTo(facebookButton.Bottom()).Offset(10);
                make.CenterX.EqualTo(View.CenterX());
                make.Left.EqualTo(View.Left()).Offset(10);
                make.Right.EqualTo(View.Right()).Offset(-10);
            });


            visitLink.TouchUpInside += (sender, e) => UIApplication.SharedApplication.OpenUrl(new NSUrl("http://msa-texas.org"));
            facebookButton.TouchUpInside += (sender, e) => UIApplication.SharedApplication.OpenUrl(new NSUrl("https://www.facebook.com/msalonestarcouncil/"));
            twitterButton.TouchUpInside += (sender, e) => UIApplication.SharedApplication.OpenUrl(new NSUrl("https://twitter.com/msalsc"));
            contributionLink.TouchUpInside += (sender, e) => UIApplication.SharedApplication.OpenUrl(new NSUrl("https://github.com/MSA-Lone-Star-Council/Showdown"));
        }
    }
}

