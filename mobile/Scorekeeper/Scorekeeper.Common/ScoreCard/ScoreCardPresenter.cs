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
        /// Updates the score on the view before sending it to the server.
        /// PostScoreAsync() must be called to send the updates to the server.
        /// </summary>
        /// <param name="team">The team to increase the score for</param>
        /// <param name="delta">The amount to update the score by</param>
        public void UpdateScore(Team team, int delta)
        {
            if (View == null) return;
            if (delta == 0) return;

            // TODO: Update a new view that shows the expected new score, 
            // before they click the "Post" button to send it to the server

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
		/// Posts the score update to the server, and updates the scores locally with the change
		/// </summary>

		private async Task PostScoreUpdateAsync()
		{
			// TODO: Actually communicate with the server. Just compute the new score right away
			int newScore = await Task.FromResult<int>(10);
		}
	}
}
