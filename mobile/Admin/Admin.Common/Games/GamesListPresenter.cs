using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin.Common.API;
using Admin.Common.API.Entities;
using Common.Common;
using ClientModel = Common.Common.Models;

namespace Admin.Common
{
	public class GamesListPresenter : Presenter<IGamesListView>
	{
		private readonly AdminRESTClient _client;

		public List<Game> _games;
		public Dictionary<string, ClientModel.School> _schools;
		public Dictionary<int?, Event> _events;

		public GamesListPresenter(AdminRESTClient client)
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

		public void OnClick(Game row)
		{
			View.OpenGame(row);
		}

		public void OnClickAdd()
		{
			View.OpenGame(new Game() { InProgress = true });
		}

		public Game GetGame(int index)
		{
			return _games[index];
		}

		public ClientModel.School GetSchool(string schoolSlug)
		{
			return _schools[schoolSlug];
		}

		public Event GetEvent(int eventId)
		{
			return _events[eventId];
		}

		public int GetNumGames()
		{
			if (_games != null)
				return _games.Count;
			return 0;
		}

		private async Task UpdateFromServer()
		{
			var gamesTask = _client.GetGames();
			var eventsTask = _client.GetEvents();
			var schoolsTask = _client.GetSchools();

			await Task.WhenAll(new Task[] { gamesTask, eventsTask, schoolsTask });

			_games = await gamesTask;

			var schools = await schoolsTask;
			_schools = schools.ToDictionary(s => s.Slug);

			var events = await eventsTask;
			_events = events.ToDictionary(e => e.Id);

			if (View != null) View.Refresh();
		}

}
}
