using System;
using Admin.Common.API.Entities;

namespace Admin.Common
{
	public interface IGamesListView
	{
		void Refresh();

		void OpenGame(Game row);
	}
}
