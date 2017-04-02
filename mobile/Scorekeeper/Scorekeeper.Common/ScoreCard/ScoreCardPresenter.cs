using System;
using System.Threading.Tasks;
using Common.Common;
using Common.Common.Models;

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

            View.HomeTeamName = View.Game.HomeTeamId;
            View.AwayTeamName = View.Game.AwayTeamId;

			// TODO: Read score from server
			View.HomeScore = await Task.FromResult<int>(10);
			View.AwayScore = await Task.FromResult<int>(10);

            View.HomeScoreDelta = 0;
            View.AwayScoreDelta = 0;
		}

        /// <summary>
        /// Updates the score on the view before sending it to the server.
        /// PostScoreAsync() must be called to send the updates to the server.
        /// </summary>
        /// <param name="team">The team to increase the score for</param>
        /// <param name="delta">The amount to update the score by</param>
        public void UpdateScore(Team team, int delta)
        {
            if (View == null) return;

            if (team == Team.Home)
            {
                View.HomeScoreDelta += delta;
            } else
            {
                View.AwayScoreDelta += delta;
            }
        }

		/// <summary>
		/// Posts the score update to the server, and updates the scores locally with the change
		/// </summary>

		private async Task PostScoreUpdateAsync()
		{
            int newHomePoints = View.HomeScore + View.HomeScoreDelta;
            int newAwayPoints = View.AwayScore + View.AwayScoreDelta;

            Score newScore = new Score
            {
                Time = DateTimeOffset.Now,
                HomePoints = newHomePoints,
                AwayPoints = newAwayPoints
            };

			// TODO: Actually communicate with the server. Just compute the new score right away
			int temp = await Task.FromResult<int>(10);

            View.HomeScore = newHomePoints;
            View.AwayScore = newAwayPoints;
            View.HomeScoreDelta = 0;
            View.AwayScoreDelta = 0;
		}
	}
}
