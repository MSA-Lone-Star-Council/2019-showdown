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
			View.OpenGame(row.ID);
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

			var baseTime = DateTimeOffset.Now;


			// TODO: Hit the backend
			Event basketball = new Event() { Title = "Brother's Basketball" };
			Event volleyball = new Event() { Title = "Sister's Vollyeball" };

			Game bballLoserFinals = new Game() { 
				Title = "Loser Bracket Finals", 
				Time = baseTime.Subtract(TimeSpan.FromSeconds(30*60 + 12)),
				Event = basketball
			};
			Game bballFinals = new Game() { 
				Title = "Finals", 
				Time = baseTime.Subtract(TimeSpan.FromSeconds(45*60 + 28)),
				Event = basketball
			};

			Score utAustinBBallScore = new Score() { Team = "UT Austin", Points = 90 };
			Score utDallasBBallScore = new Score() { Team = "UT Dallas", Points = 10 };


			Score uHoustonBBallScore = new Score() { Team = "UH", Points = 40 };
			Score texasTechBBallScore = new Score() { Team = "Texas Tech", Points = 30 };

			bballFinals.Score = new List<Score>() { utAustinBBallScore, utDallasBBallScore };
			bballFinals.Teams = new List<string>() { "UT Austin", "UT Dallas" };

			bballLoserFinals.Score = new List<Score> { uHoustonBBallScore, texasTechBBallScore };

			Game volleyballElim = new Game() { 
				Title = "SMU vs UTA Elimination", 
				Time = baseTime.Subtract(TimeSpan.FromSeconds(20*60 + 48)),
				Event = volleyball
			};
			Score smuScore = new Score() { Team = "SMU", Points = 25 };
			Score utaScore = new Score() { Team = "UT Arlington", Points = 10 };
			volleyballElim.Score = new List<Score> { smuScore, utaScore };
			volleyballElim.Teams = new List<string>() { "SMU", "UT Arlington" };


			return new List<Game>() { bballLoserFinals, bballFinals, volleyballElim };
		}
	}
}
