using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Common;
using Common.Common.Models;

namespace Client.Common
{
	public class EventPresenter : Presenter<IEventView>
	{
		ShowdownRESTClient client;

		public Event Event { get; set; }
		public List<Game> games;

		public EventPresenter(ShowdownRESTClient backendClient)
		{
			this.client = backendClient;
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

		public void OnClickRow(int row)
		{
			View.OpenGame(games[row]);
		}

		public Game GetGame(int row)
		{
			return games[row];
		}

		public int GetGameCount()
		{
			return games.Count;
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
	}
}
