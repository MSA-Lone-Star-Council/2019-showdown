using System;
using System.Collections.Generic;

namespace Common.Common.Models
{
	public struct Game
	{
		public string ID { get; set; }
		public string Title { get; set; }
		public Event Event { get; set; }

		public List<string> Teams { get; set; }

		/// <summary>
		/// The latest score for the game. 
		/// It is essentially a list of (Team, Points) pairs (it's pretty bad naming)
		/// 
		/// This value can be null, and likely is
		/// </summary>
		/// <value>The score.</value>
		public List<Score> Score { get; set; }

		// Map this to JSON field "Scores" - the backend got all wonky...
		/// <summary>
		/// The score history of the game, with the newest score first
		/// 
		/// This value can be null, but usually isn't
		/// </summary>
		/// <value>The score history.</value>
		public List<ScoreRecord> ScoreHistory { get; set; }

		public DateTimeOffset Time { get; set; }
	}
}
