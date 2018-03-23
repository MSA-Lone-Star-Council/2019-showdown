using System;
using System.Collections.Generic;
using Common.Common.Models;

namespace Client.Common
{
	public interface IAnnouncementsView
	{
	    List<Announcement> Announcements { set; }

		void OpenNewAnnouncement();
		void ShowMessage(string v);
	}
}
