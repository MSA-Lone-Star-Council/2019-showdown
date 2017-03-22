using System;
using System.Collections.Generic;

namespace Common.Common.Models
{
	public class Game
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

		public DateTimeOffset Time { get; set; }
	}
}
