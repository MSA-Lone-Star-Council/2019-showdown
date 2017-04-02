using System;
using System.Collections.Generic;
using Common.Common.Models;

namespace Client.Common
{
	public interface ISportsView
	{
		void OpenGame(Game g);

		void ShowMessage(string message);

		void Refresh();
	}
}
