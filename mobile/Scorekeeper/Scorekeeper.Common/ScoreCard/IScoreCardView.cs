using System;
using Common.Common;
using Common.Common.Models;

namespace Scorekeeper.Common
{
	public interface IScoreCardView
	{
        Game Game { get; set; }

        string HomeTeamName { get; set; }
        string AwayTeamName { get; set; }

		int HomeScore { get; set; }
		int AwayScore { get; set; }
        int HomeScoreDelta { get; set; }
        int AwayScoreDelta { get; set; }

        bool CanPostScore { set; }

        void EndGame();
	}
}
