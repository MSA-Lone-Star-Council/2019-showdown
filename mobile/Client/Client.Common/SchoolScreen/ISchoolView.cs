using System;
using Common.Common.Models;

namespace Client.Common
{
	public interface ISchoolView
	{
		School School { set; }
		void Refresh();
	}
}
