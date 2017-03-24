using System;
using System.Collections.Generic;

namespace Common.Common.Models
{
	/// <summary>
	/// Represents the score of a game at a particular instance in time
	/// </summary>
	public class ScoreRecord
	{
		public string ID { get; set; }
		public string Game { get; set; }

		public DateTimeOffset Time { get; set; }
		public List<Score> Scores { get; set; }
	}
}
