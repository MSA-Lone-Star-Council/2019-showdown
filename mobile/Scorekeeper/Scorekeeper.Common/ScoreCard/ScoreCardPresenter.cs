using System;
using System.Threading.Tasks;
using Common.Common;

namespace Scorekeeper.Common
{
	/// <summary>
	/// Presenter for a Versus Card score tracker
	/// Implements the business logic of displaying the score for a "home" and "away" team
	/// </summary>
	public class ScoreCardPresenter : Presenter<IScoreCardView>
	{
		/// <summary>
		/// Enum representing whether a team is Home or Away
		/// </summary>
		public enum Team { Home, Away };

		/// <summary>
		/// Setups the view. Loads cached values immediately and then gets the latest score from the server
		/// </summary>
		public async Task SetupView()
		{
			if (View == null) return;

			// TODO: Read scores from cache
			SetScoreOnView(Team.Home, 0);
			SetScoreOnView(Team.Away, 0);

			// TODO: Read score from server
			int homeScore = await Task.FromResult<int>(10);
			int awayScore = await Task.FromResult<int>(10);

			SetScoreOnView(Team.Home, homeScore);
			SetScoreOnView(Team.Away, awayScore);

		}

		/// <summary>
		/// Increases the score. Optimistically updates the cache and the view with
		/// the expected score and then updates it with the score from the server
		/// </summary>
		/// <param name="team">The team to increase the score for</param>
		/// <param name="delta">The amount to increase (or if negative, decrease)</param>
		public async Task IncreaseScore(Team team, int delta)
		{
			if (View == null) return; 

			int previousScore = GetScoreFromView(team);

			// Shoot off an update to the server
			var scoreRequest = PostScoreUpdateAsync(team, delta);

			// Compute what the score should be (for immediate update)
			int expectedScore = previousScore + delta;
			SetScoreOnView(team, expectedScore);

			// Wait for the server to respond with the authoratitive score
			int actualScore = await scoreRequest;
			SetScoreOnView(team, actualScore);

			// TODO: Cache the score
		}

		/// <summary>
		/// Gets the score currently displayed on the view
		/// </summary>
		/// <returns>The score from view.</returns>
		/// <param name="team">The team to get the score for</param>
		private int GetScoreFromView(Team team)
		{
			return team == Team.Home ? View.HomeScore : View.AwayScore;
		}

		/// <summary>
		/// Sets the score on the view.
		/// </summary>
		/// <param name="team">The team to set the score for</param>
		/// <param name="score">The score to set it to</param>
		private void SetScoreOnView(Team team, int score)
		{
			switch(team)
			{
				case Team.Home: View.HomeScore =  score; break;
				case Team.Away: View.AwayScore = score; break;
			}
		}

		/// <summary>
		/// Posts the score update to the server
		/// </summary>
		/// <returns>The authoratitive score for the team</returns>
		/// <param name="team">Team. The team to update</param>
		/// <param name="delta">Delta. The amount to change the score</param>
		private async Task<int> PostScoreUpdateAsync(Team team, int delta)
		{
			int previousScore = GetScoreFromView(team);

			// TODO: Actually communicate with the server. Just compute the new score right away
			int newScore = await Task.FromResult<int>(previousScore + delta);

			return newScore;
		}
	}
}
