using System;
using System.Threading.Tasks;
using Admin.Common.API;
using Admin.Common.API.Entities;
using Common.Common;

namespace Admin.Common
{
	public class LocationsListPresenter : Presenter<ILocationsListView>
	{
		AdminRESTClient _client;

		public LocationsListPresenter(AdminRESTClient backendClient)
		{
			_client = backendClient;
		}

		public async Task OnBegin()
		{
			await UpdateFromServer();
		}

		public async Task OnTick()
		{
			await UpdateFromServer();
		}

		public void OnClickRow(Location row)
		{
			View.OpenLocation(row);
		}

		public void OnClickAdd()
		{
			var location = new Location()
			{
				Name = "Untitled Location",
				Address = "Enter address",
				Notes = "Describe the place",
			};
			View.OpenLocation(location);
		}

		private async Task UpdateFromServer()
		{
			var locations = await _client.GetLocations();

			if (View != null)
				View.Locations = locations;
		}
	}
}
