using System;
using Common.Common.Models;

namespace Client.Common
{
	public interface IEventView
	{
		void OpenGame(Game game);
		void Refresh(Event e);
		void ScheduleReminder(Event e);

		void ShowMessage(string message);
	}
}
