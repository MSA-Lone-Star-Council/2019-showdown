using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Common;
using Common.Common.Models;

namespace Client.Common
{
	public class SchoolPresenter : Presenter<ISchoolView>, IGameHavingPresenter
	{
		ShowdownRESTClient client;
		SubscriptionManager manager;
		public School School { get; set; }
		public List<Game> games;

		public SchoolPresenter(ShowdownRESTClient backendClient, SubscriptionManager subscriptionManager)
		{
			client = backendClient;
			games = new List<Game>();
			manager = subscriptionManager;
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

		public void OnClickRow(int row)
		{
			View.OpenGame(games[row]);
		}

		public Game GetGame(int row)
		{
			return games[row];
		}

		public int GameCount()
		{
			return games.Count;
		}

		private async Task UpdateFromServer()
		{
			var gamesFromServer = await client.GetSchoolGames(School.Slug);
			games = gamesFromServer.OrderByDescending(g => g.Score.Time).ToList();
			if (View != null)
			{
				View.Refresh();
			}
		}

		public void GameTapped(int index)
		{
			View.OpenGame(games[index]);
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
