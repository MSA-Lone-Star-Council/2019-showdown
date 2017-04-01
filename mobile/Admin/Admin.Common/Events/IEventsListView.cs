using System;
using System.Collections.Generic;
using Admin.Common.API.Entities;

namespace Admin.Common
{
	public interface IEventsListView
	{
		List<Event> Events { set; }

		void OpenEvent(Event row);

	}
}
