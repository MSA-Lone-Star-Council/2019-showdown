using System;
using System.Threading.Tasks;
using Common.Common;
using Common.Common.Models;
using Admin.Common.API;

namespace Scorekeeper.Common
{
	/// <summary>
	/// Presenter for a Versus Card score tracker
	/// Implements the business logic of displaying the score for a "home" and "away" team
	/// </summary>
	public class ScoreCardPresenter : Presenter<IScoreCardView>
	{

        private readonly AdminRESTClient _client;

        /// <summary>
        /// Enum representing whether a team is Home or Away
        /// </summary>
        public enum Team { Home, Away };

		/// <summary>
		/// Setups the view. Loads cached values immediately and then gets the latest score from the server
		/// </summary>
    
        public ScoreCardPresenter(AdminRESTClient client)
        {
            _client = client;
        }
        
		public void SetupView()
		{
			if (View == null) return;

            View.AwayTeamName = View.Game.AwayTeam.ShortName;
            View.HomeTeamName = View.Game.HomeTeam.ShortName;

            View.AwayScore = (int)View.Game.Score.AwayPoints;
            View.HomeScore = (int)View.Game.Score.HomePoints;

            View.HomeScoreDelta = 0;
            View.AwayScoreDelta = 0;

            if (View.Game.InProgress == false)
            {
                View.EndGame();
            }
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
            
            //If there's any change in score, the View can post updates.
            View.CanPostScore = (View.AwayScoreDelta != 0 | View.HomeScoreDelta != 0);
        }

        /// <summary>
        /// Posts the score update to the server, and updates the scores locally with the change
        /// </summary>

        public async Task PostScoreUpdateAsync()
		{
            int newHomePoints = View.HomeScore + View.HomeScoreDelta;
            int newAwayPoints = View.AwayScore + View.AwayScoreDelta;

            Score newScore = new Score
            {
                HomePoints = newHomePoints,
                AwayPoints = newAwayPoints
            };

            var scoreFromServer = await _client.SaveScore(View.Game, newScore);

            View.HomeScore = (int) scoreFromServer.HomePoints;
            View.AwayScore = (int) scoreFromServer.AwayPoints;
            View.HomeScoreDelta = 0;
            View.AwayScoreDelta = 0;

            View.CanPostScore = false;
		}

        public async Task EndGameAsync()
        {
            await _client.EndGame(View.Game);
            View.CanPostScore = false;
            View.EndGame();
        }

    }
}
