using System;
using System.Threading.Tasks;
using Admin.Common.API;
using Common.Common;

namespace Admin.Common
{
	public class AnnouncementEditorPresenter : Presenter<IAnnouncementEditorView>
	{
		AdminRESTClient _client;

		public AnnouncementEditorPresenter(AdminRESTClient client)
		{
			_client = client;
		}

		public async Task Save()
		{
			if (View != null)
			{
				await _client.CreateAnnouncement(View.Announcement);
			}

			if (View != null)
			{
				View.GoBack();
			}
		}
	}
}
