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
		Event _event;
		List<Location> _locations;
		AdminRESTClient _client;


		public EventDetailPresenter(Event e, AdminRESTClient client)
		{
			_client = client;
			_event = e;
		}

		public override void TakeView(IEventDetailView view)
		{
			base.TakeView(view);
			View.Event = _event;
		}

		public async Task OnBegin()
		{
			await UpdateLocationsFromServer();
		}

		async Task UpdateLocationsFromServer()
		{
			if (View != null) View.LocationLoading = true;
			_locations = await _client.GetLocations();
			if (View != null)
			{
				View.LocationLoading = false;
				View.SelectedLocationIndex = _locations.FindIndex(l => l.Id == _event.LocationId);
			}
		}

		public async Task Save()
		{
			var eventToSave = View.Event;
			eventToSave.Id = _event.Id;
			eventToSave.LocationId = (int) _locations[View.SelectedLocationIndex].Id;

			_event = await _client.SaveEvent(eventToSave);
			await UpdateLocationsFromServer();
		}

		public string GetLocationName(int row)
		{
			return _locations[row].Name;
		}

		public int GetLocationCount()
		{
			return _locations.Count;
		}
	}
}
