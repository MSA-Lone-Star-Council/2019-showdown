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

		public async Task OnBegin()
		{
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
			List<ScoreRecord> result = new List<ScoreRecord>();
			Random random = new Random();

			var currentTime = new DateTimeOffset(2017, 4, 17, 10, 5, 32, TimeSpan.FromHours(-5));

			int numScores = 60;

			int awayScore = 0;
			int homeScore = 0;

			var awayTeam = Game.Teams[0];
			var homeTeam = Game.Teams[1];

			for (int i = 0; i < numScores; i++)
			{
				Score away = new Score() { Team = awayTeam, Points = awayScore };
				Score home = new Score() { Team = homeTeam, Points = homeScore };

				ScoreRecord record = new ScoreRecord() { Time = currentTime };
				record.Scores = new List<Score>() { away, home };

				result.Insert(0, record);

				// Advance simulation
				currentTime = currentTime.Add(TimeSpan.FromSeconds(random.Next(20, 180)));

				if (random.NextDouble() < 0.5)
				{
					awayScore += 2;
				}
				else
				{
					homeScore += 2;
				}
			}

			return result;
		}
	}
}
