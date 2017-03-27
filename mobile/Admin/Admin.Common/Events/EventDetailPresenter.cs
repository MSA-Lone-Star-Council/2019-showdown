using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin.Common.API;
using Admin.Common.API.Entities;
using Common.Common;

namespace Admin.Common
{
	public class EventDetailPresenter : Presenter<IEventDetailView>
	{
		public Event Event { get; set; }
		List<Location> _locations;

		AdminRESTClient _client;

		public EventDetailPresenter(AdminRESTClient client)
		{
			_client = client;
		}

		public async Task OnBegin()
		{
			if(View != null) View.LocationOptions = new List<string>();

			await UpdateLocationsFromServer();

		}

		async Task UpdateLocationsFromServer()
		{
			if (View != null) View.LocationLoading = true;
			_locations = await _client.GetLocations();
			if (View != null)
			{
				View.LocationOptions = _locations.Select(l => l.Name).ToList();
				View.SelectedLocationIndex = _locations.Select(l => l.Id).ToList().IndexOf(Event.LocationId);
				View.LocationLoading = false;
			}

		}

		public async Task Save(Event updatedEvent)
		{
			updatedEvent.LocationId = _locations[updatedEvent.LocationId].Id; // It's a hack...
			Event = await _client.SaveEvent(updatedEvent);
			await UpdateLocationsFromServer();
		}
	}
}
