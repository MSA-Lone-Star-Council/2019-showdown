using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Common;
using Common.Common.Models;

namespace Client.Common
{
	public class GamePresenter : Presenter<IGameView>
	{
		public Game Game { get; set; }

		public ShowdownRESTClient _client;

		public GamePresenter(ShowdownRESTClient client)
		{
			_client = client;
		}

		public async Task OnBegin()
		{
			if (View != null)
				View.ScoreHistory = new List<ScoreRecord>();
			await UpdateFromServer();
		}

		public async Task OnTick()
		{
			await UpdateFromServer();
		}


		public async Task OnStar()
		{
			View.ShowMessage("Subscribed for notifications for this game!");
			// TODO: What the message says
		}

		async Task UpdateFromServer()
		{
			var scoreHistory = await GetScoreHistoryFromServer();

			if (View != null) View.ScoreHistory = scoreHistory;
		}

		async Task<List<ScoreRecord>> GetScoreHistoryFromServer()
		{
			return await _client.GetScoreHistory(Game.ID);
		}
	}
}
