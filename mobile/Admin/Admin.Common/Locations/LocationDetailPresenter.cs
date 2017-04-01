using System;
using System.Threading.Tasks;
using Admin.Common.API;
using Admin.Common.API.Entities;
using Common.Common;

namespace Admin.Common
{
	public class LocationDetailPresenter : Presenter<ILocationDetailView>
	{
		Location _location;
		AdminRESTClient _client;

		public LocationDetailPresenter(Location location, AdminRESTClient client)
		{
			_client = client;
			_location = location;
		}

		public override void TakeView(ILocationDetailView view)
		{
			base.TakeView(view);
			View.Location = _location;
		}

		public async Task Save()
		{

			if (View != null)
			{
				var locationToSave = View.Location;
				locationToSave.Id = _location.Id;
				View.LocationSaving = true;
				_location = await _client.SaveLocation(locationToSave);
			}
			if (View != null)
			{
				View.Location = _location;
				View.LocationSaving = false;
			}
		}

		public async Task Delete()
		{
			await _client.DeleteLocation(_location);
			View.GoBack();
		}
	}
}
