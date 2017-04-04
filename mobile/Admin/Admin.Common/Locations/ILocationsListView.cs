using System;
using System.Collections.Generic;
using Admin.Common.API.Entities;

namespace Admin.Common
{
	public interface ILocationsListView
	{
		List<Location> Locations { set; }
		void OpenLocation(Location row);
	}
}
