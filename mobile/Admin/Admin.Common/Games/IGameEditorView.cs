using System;
using Admin.Common.API.Entities;

namespace Admin.Common
{
	public interface IGameEditorView
	{
		Game Game { get; set; }

		int SelectedEventIndex { get; set; }
		int SelectedScorekeeperIndex { get; set; }
		int SelectedAwayTeamIndex { get; set; }
		int SelectedHomeTeamIndex { get; set; }

		bool FetchingValues { set; }

		void GoBack();
	}
}
