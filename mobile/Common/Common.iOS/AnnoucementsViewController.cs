using System;
using System.Collections.Generic;
using Client.Common;
using Common.Common;
using Common.Common.Models;
using Foundation;
using UIKit;

namespace Common.iOS
{
	public class AnnoucementsViewController : UIViewController, IAnnouncementsView
	{
	    static NSString AnnouncmentCellID = new NSString("AnnouncementCellID");

	    public UITableView AnnouncementsList { get; set; }

		public AnnouncementsPresenter Presenter { get; set; }

		NSTimer timer;

	    public AnnoucementsViewController(IAnnoucementInteractor client)
		{
		    Presenter = new AnnouncementsPresenter(client);
			Presenter.TakeView(this);
            this.Title = "Announcements";

		}

	    public List<Announcement> Announcements
	    {
	        set
	        {
				if (AnnouncementsList == null) return;
                AnnouncementsTableSource ats = AnnouncementsList.Source as AnnouncementsTableSource;
	            ats.Announcements = value;
	            AnnouncementsList.ReloadData();
	        }
	    }

	    public async override void ViewDidLoad()
	    {
	        base.ViewDidLoad();
            View.BackgroundColor = UIColor.Clear;

	        AnnouncementsList = new UITableView(View.Bounds)
	        {
                BackgroundColor = UIColor.White,
	            Source = new AnnouncementsTableSource(),
	            RowHeight = 100,
                SeparatorStyle = UITableViewCellSeparatorStyle.SingleLineEtched
	        };
			AnnouncementsList.RegisterClassForCellReuse(typeof(AnnouncementCell), AnnouncmentCellID);

	        View.AddSubview(AnnouncementsList);
	    }

	    public override async void ViewDidAppear(bool animated)
	    {
	        base.ViewDidAppear(animated);
			Presenter.TakeView(this);
			await Presenter.OnBegin();
			timer = NSTimer.CreateRepeatingScheduledTimer(TimeSpan.FromSeconds(10), async (obj) => await Presenter.OnTick());
	    }

	    public override void ViewWillDisappear(bool animated)
	    {
	        base.ViewWillDisappear(animated);
	        Presenter.RemoveView();
			if (timer != null) timer.Invalidate();
	    }

		public virtual void OpenNewAnnouncement()
		{
			throw new NotImplementedException();
		}

		class AnnouncementsTableSource : UITableViewSource
		{
			public List<Announcement> Announcements { get; set; }

		    public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell(AnnouncmentCellID) as AnnouncementCell;
				cell.BackgroundColor = UIColor.Clear;

				var announcement = Announcements[indexPath.Row];

				cell.UpdateCell(announcement);

				return cell;
			}

			public override nint RowsInSection(UITableView tableview, nint section)
			{
			    return Announcements?.Count ?? 0;
			}

			public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
			{
				tableView.DeselectRow(indexPath, false);
			}
		}
	}
}
