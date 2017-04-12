using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Common
{
	public interface INotificationHub
	{
		Task SaveTags(List<string> tags);
	}
}
