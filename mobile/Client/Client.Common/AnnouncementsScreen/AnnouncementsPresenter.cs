using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Common;
using Common.Common.Models;

namespace Client.Common
{
	public class AnnouncementsPresenter : Presenter<IAnnouncementsView>
	{
		private readonly ShowdownRESTClient _client;

		private List<Announcement> _announcements = new List<Announcement>();

	    public AnnouncementsPresenter(ShowdownRESTClient client)
	    {
	        _client = client;
	    }

		public override void TakeView(IAnnouncementsView view)
		{
			base.TakeView(view);
			View.Announcements = _announcements;
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
