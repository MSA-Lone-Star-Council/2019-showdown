using System;
using System.Threading.Tasks;
using Admin.Common.API;
using Admin.Common.API.Entities;
using Common.Common;

namespace Admin.Common
{
	public class EventsListPresenter : Presenter<IEventsListView>
	{
		private readonly AdminRESTClient _client;

		public EventsListPresenter(AdminRESTClient client)
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

		public void OnClickRow(Event row)
		{
			View.OpenEvent(row);
		}

		private async Task UpdateFromServer()
		{
			var events = await _client.GetEvents();

			if (View != null)
				View.Events = events;
		}
	}
}
