using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Common.Common.Models
{
	public struct Game
	{
		public string ID { get; set; }
		public string Title { get; set; }

		[JsonProperty(PropertyName = "event")]
		public int EventId { get; set; }

		public List<string> Teams { get; set; }

		/// <summary>
		/// The latest score for the game. 
		/// It is essentially a list of (Team, Points) pairs (it's pretty bad naming)
		/// </summary>
		/// <value>The score.</value>
		public List<Score> Score { get; set; }

		public DateTimeOffset Time { get; set; }

	    public static Game FromJSON(string jsonString)
	    {
	        return JsonConvert.DeserializeObject<Game>(jsonString);
	    }

	    public static List<Game> FromJSONArray(string jsonString)
	    {
	        return JsonConvert.DeserializeObject<List<Game>>(jsonString);
	    }

	    public bool Equals(Game other)
	    {
	        return (
                ID == other.ID &&
                Title == other.Title &&
	            EventId == other.EventId &&
                Time == other.Time &&
                Teams.SequenceEqual(other.Teams) &&
                Score.SequenceEqual(other.Score)
	        );
	    }

	    public override bool Equals(object obj)
	    {
	        if (ReferenceEquals(null, obj)) return false;
	        return obj is Game && Equals((Game) obj);
	    }

	    public static bool operator ==(Game left, Game right)
	    {
	        return left.Equals(right);
	    }

	    public static bool operator !=(Game left, Game right)
	    {
	        return !left.Equals(right);
	    }
	}
}
