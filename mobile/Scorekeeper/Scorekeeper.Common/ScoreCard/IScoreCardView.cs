using System;
using Common.Common;

namespace Scorekeeper.Common
{
	public interface IScoreCardView
	{
		int HomeScore { get; set; }
		int AwayScore { get; set; }
	}
}
