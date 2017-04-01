using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Common;
using Common.Common.Models;

namespace Client.Common
{
	public class SchoolPresenter : Presenter<ISchoolView>
	{
		ShowdownRESTClient client;
		public School School { get; set; }
		public List<Game> games;

		public SchoolPresenter(ShowdownRESTClient backendClient)
		{
			client = backendClient;
			games = new List<Game>();
		}

		public async Task OnBegin()
		{
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
			var gamesFromServer = await client.GetSchoolGames(School.Slug);
			games = gamesFromServer.OrderByDescending(g => g.Score.Time).ToList();
			if (View != null)
			{
				View.Refresh();
			}
		}
	}
}
