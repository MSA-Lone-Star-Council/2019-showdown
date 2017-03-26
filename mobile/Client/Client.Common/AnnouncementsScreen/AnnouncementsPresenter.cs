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

	    public AnnouncementsPresenter(ShowdownRESTClient client)
	    {
	        _client = client;
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
		    var announcements = await _client.GetAnnouncements();
		    if (View != null)
		        View.Announcements = announcements;
		}
	}
}
