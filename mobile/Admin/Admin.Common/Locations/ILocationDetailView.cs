using System;
using Admin.Common.API.Entities;

namespace Admin.Common
{
	public interface ILocationDetailView
	{
		Location Location { get; set; }

		bool LocationSaving { set; }

		void GoBack();
	}
}
