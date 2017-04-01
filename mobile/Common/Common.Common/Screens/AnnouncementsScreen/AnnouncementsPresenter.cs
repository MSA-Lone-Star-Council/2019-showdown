using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Common;
using Common.Common.Models;

namespace Client.Common
{
	public class AnnouncementsPresenter : Presenter<IAnnouncementsView>
	{
		private readonly IAnnoucementInteractor _client;

		private List<Announcement> _announcements = new List<Announcement>();

		public AnnouncementsPresenter(IAnnoucementInteractor client)
	    {
	        _client = client;
	    }

		public override void TakeView(IAnnouncementsView view)
		{
			base.TakeView(view);
			View.Announcements = _announcements;
		}

		public void OnClickAdd()
		{
			View.OpenNewAnnouncement();
		}

		public async Task OnBegin()
		{
			await UpdateFromServer();
		}

		public async Task OnTick()
		{
			await UpdateFromServer();
		}

		private async Task UpdateFromServer()
		{
		    _announcements = await _client.GetAnnouncements();
		    if (View != null)
		        View.Announcements = _announcements;
		}
	}
}
