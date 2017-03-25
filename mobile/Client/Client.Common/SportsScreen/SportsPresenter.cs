using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Common;
using Common.Common.Models;

namespace Client.Common
{
	public class SportsPresenter : Presenter<ISportsView>
	{

	    private readonly ShowdownRESTClient _client;

	    public SportsPresenter(ShowdownRESTClient client)
	    {
	        _client = client;
	    }

		public async Task OnBegin()
		{
			await UpdateFromServer();
		}

		public async Task OnTick()
		{
			await UpdateFromServer();
		}

		public async Task OnClickRow(Game row)
		{
			View.OpenGame(row);
		}

		public async Task OnStar(Game game)
		{
			View.ShowMessage("Subscribed for notifications for this game!");
			// TODO: What the message says
		}

		private async Task UpdateFromServer()
		{
			var gamesFromServer = await GetAllGames();

			if (View != null)
				View.Games = gamesFromServer.OrderByDescending(g => g.Time).ToList();
		}

		private async Task<List<Game>> GetAllGames()
		{
		    return await _client.GetAllGames();
		}
	}
}
