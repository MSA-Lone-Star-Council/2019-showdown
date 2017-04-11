using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Common;
using Common.Common.Models;

namespace Client.Common
{
	public class SportsPresenter : Presenter<ISportsView>, IGameHavingPresenter
	{
	    private readonly ShowdownRESTClient _client;
		private readonly SubscriptionManager manager;

		private List<Game> games;

		public SportsPresenter(ShowdownRESTClient client, SubscriptionManager manager)
	    {
	        _client = client;
			games = new List<Game>();
			this.manager = manager;
	    }

		public async Task OnBegin()
		{
			if (View != null) View.Refresh();
			await UpdateFromServer();
		}

		public async Task OnTick()
		{
			await UpdateFromServer();
		}

		public void OnClickRow(Game row)
		{
			View.OpenGame(row);
		}

		public int GameCount()
		{
			return games.Count;
		}

		public Game GetGame(int row)
		{
			return games[row];
		}

		public bool Subscribed(int row)
		{
			return false;
		}

		public void GameTapped(int index)
		{
			View.OpenGame(games[index]);
		}

		public async Task OnStar(Game game)
		{
			View.ShowMessage("Subscribed for notifications for this game!");
			// TODO: What the message says
		}

		private async Task UpdateFromServer()
		{
			var gamesFromServer = await GetAllGames();
			games = gamesFromServer.OrderByDescending(g => g.Score.Time).ToList();
            if (View != null)
            {
                View.Refresh();
                View.Games = gamesFromServer;
            }
		}

		private async Task<List<Game>> GetAllGames()
		{
		    return await _client.GetAllGames();
		}

		public bool IsSubscribed(int index)
		{
			var game = games[index];
			return manager[game.TopicId];
		}

		public void SubscribeTapped(int index)
		{
			manager.ToggleSubscription(games[index].TopicId);
			View.Refresh();
		}
	}
}
