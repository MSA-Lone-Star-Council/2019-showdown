using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Common.Models;

namespace Common.Common
{
	public interface IAnnoucementInteractor
	{
		Task<List<Announcement>> GetAnnouncements();
		Task<Announcement> CreateAnnouncement(Announcement announcement);
	}
}
