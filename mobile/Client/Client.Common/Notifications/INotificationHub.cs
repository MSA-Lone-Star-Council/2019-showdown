using System;
using System.Collections.Generic;

namespace Client.Common
{
	public interface INotificationHub
	{
		void SaveTags(List<string> tags);
	}
}
