using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Admin.Common.API;
using Admin.Common.API.Entities;
using Common.Common;
using School = Common.Common.Models.School;

namespace Admin.Common
{
	public class GameEditorPresenter : Presenter<IGameEditorView>
	{
		AdminRESTClient _client;
		Game _game;

		List<Event> _events;
		List<School> _schools;
		List<User> _users;

		public GameEditorPresenter(Game game, AdminRESTClient client)
		{
			_client = client;
			_game = game;

			_events = new List<Event>();
			_schools = new List<School>();
			_users = new List<User>();
		}

		public override void TakeView(IGameEditorView view)
		{
			base.TakeView(view);
			if (!_game.IsEmpty())
				View.Game = _game;
		}

		public async Task OnBegin()
		{
			await UpdateValuesFromServer();
		}

		public string GetEventName(int row)
		{
			if (_events.Count == 0) return "";
			return _events[row].Title;
		}

		public int GetEventCount()
		{
			return _events.Count;
		}

		public string GetSchoolName(int row)
		{
			if (_schools.Count == 0) return "";
			return _schools[row].ShortName;
		}

		public int GetSchoolCount()
		{
			return _schools.Count;
		}

		public string GetUserName(int row)
		{
			if (_users.Count == 0) return "";
			return _users[row].Name;
		}

		public int GetUserCount()
		{
			return _users.Count;
		}

		public async Task Save()
		{
			Game gameToSave = View.Game;
			gameToSave.Id = _game.Id;
			gameToSave.EventId = (int) _events[View.SelectedEventIndex].Id;
			gameToSave.AwayTeamId = _schools[View.SelectedAwayTeamIndex].Slug;
			gameToSave.HomeTeamId = _schools[View.SelectedHomeTeamIndex].Slug;
			gameToSave.ScorekeeperId = _users[View.SelectedScorekeeperIndex].Id;

			_game = await _client.SaveGame(gameToSave);
			await UpdateValuesFromServer();
		}

		public async Task Delete()
		{
			await _client.DeleteGame(_game);
			View.GoBack();
		}

		async Task UpdateValuesFromServer()
		{
			if (View != null) View.FetchingValues = true;

			var eventsTask = _client.GetEvents();
			var schoolsTask = _client.GetSchools();
			var usersTask = _client.GetUsers();

			await Task.WhenAll(new Task[] { eventsTask, schoolsTask, /* usersTask */ });

			_events = await eventsTask;
			_schools = await schoolsTask;
			_users = await usersTask;

			if (View != null)
			{
				View.FetchingValues = false;
				View.SelectedEventIndex = _events.FindIndex(e => e.Id == _game.EventId);
				View.SelectedAwayTeamIndex = _schools.FindIndex(s => s.Slug == _game.AwayTeamId);
				View.SelectedHomeTeamIndex = _schools.FindIndex(s => s.Slug == _game.HomeTeamId);
				View.SelectedScorekeeperIndex = _users.FindIndex(u => u.Id == _game.ScorekeeperId);
			}
		}
	}
}
