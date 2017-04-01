using System;
using Common.Common.Models;

namespace Client.Common
{
	public interface ISchoolView
	{
		void Refresh();

		void OpenGame(Game game);
	}
}
