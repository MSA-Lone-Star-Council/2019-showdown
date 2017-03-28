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

		public void OnClickAdd()
		{
			var newEvent = new Event()
			{
				Title = "Untitled Event",
				Audience = "general",
				StartTime = DateTimeOffset.Now,
				EndTime = DateTimeOffset.Now.AddHours(1),
				Description = "No description",
				LocationId = 1,
			};
			View.OpenEvent(newEvent);
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
