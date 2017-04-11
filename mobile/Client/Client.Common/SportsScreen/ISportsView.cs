using System;
using System.Collections.Generic;
using Common.Common.Models;

namespace Client.Common
{
	public interface ISportsView
	{
        List<Game> Games { get; set; }

        void OpenGame(Game g);

		void ShowMessage(string message);

		void Refresh();
	}
}
