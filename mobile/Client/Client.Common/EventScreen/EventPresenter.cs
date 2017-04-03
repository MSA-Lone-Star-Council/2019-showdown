using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Common;
using Common.Common.Models;

namespace Client.Common
{
	public class EventPresenter : Presenter<IEventView>, IGameHavingPresenter
	{
		ShowdownRESTClient client;
		SubscriptionManager manager;

		public Event Event { get; set; }
		public List<Game> games;

		public EventPresenter(ShowdownRESTClient backendClient, SubscriptionManager subscriptionManager)
		{
			this.client = backendClient;
			this.manager = subscriptionManager;
			games = new List<Game>();
		}

		public async Task OnBegin()
		{
			View.Refresh(Event);
			await UpdateFromServer();
		}

		public async Task OnTick()
		{
			await UpdateFromServer();
		}

		public Game GetGame(int row)
		{
			return games[row];
		}

		public int GameCount()
		{
			return games.Count;
		}

		public void GameTapped(int index)
		{
			View.OpenGame(games[index]);
		}

		private async Task UpdateFromServer()
		{
			var gamesFromServer = await client.GetEventGames(Event.Id);
			games = gamesFromServer.OrderByDescending(g => g.Score.Time).ToList();
			if (View != null)
			{
				View.Refresh(Event);

			}
		}

		public bool IsSubscribedToEvent(Event e)
		{
			return manager[e.TopicId];
		}

		public bool IsSubscribed(int index)
		{
			var game = games[index];
			return manager[game.TopicId];
		}

		public void SubscribeTapped(int index)
		{
			manager.ToggleSubscription(games[index].TopicId);
			View.Refresh(Event);
		}

		public void EventSubscribeTapped()
		{
			manager.ToggleSubscription(Event.TopicId);
			View.Refresh(Event);
		}
	}
}
