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
		public SubscriptionManager manager;

		public GamePresenter(ShowdownRESTClient client, SubscriptionManager manager)
		{
			_client = client;
			this.manager = manager;
		}

		public async Task OnBegin()
		{
			if (View != null)
				View.ScoreHistory = new List<Score>();
			await UpdateFromServer();
		}

		public async Task OnTick()
		{
			await UpdateFromServer();
		}


		public void OnStar()
		{
			//View.ShowMessage("Subscribed for notifications for this game!");
			manager.ToggleSubscription(Game.TopicId);
			View.Refresh();
		}

		public bool IsSubscribed()
		{
			return manager[Game.TopicId];
		}

		async Task UpdateFromServer()
		{
			var scoreHistory = await GetScoreHistoryFromServer();

			if (View != null) View.ScoreHistory = scoreHistory;
		}

		async Task<List<Score>> GetScoreHistoryFromServer()
		{
			return await _client.GetScoreHistory(Game.ID);
		}
	}
}
