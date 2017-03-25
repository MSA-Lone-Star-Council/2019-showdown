using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Common.Common.Models
{
	public struct Score
	{
		public string Team { get; set; }

		[JsonProperty(PropertyName = "score")]
		public double Points { get; set; }

		public static Score FromJSON(string jsonString)
		{
			return JsonConvert.DeserializeObject<Score>(jsonString);
		}

		public static List<Score> FromJSONArray(string jsonString)
		{
			return JsonConvert.DeserializeObject<List<Score>>(jsonString);
		}

	    public bool Equals(Score other)
	    {
	        return Team == other.Team && Points == other.Points;
	    }

	    public override bool Equals(object obj)
	    {
	        if (ReferenceEquals(null, obj)) return false;
	        return obj is Score && Equals((Score) obj);
	    }

	    public static bool operator ==(Score left, Score right)
	    {
	        return left.Equals(right);
	    }

	    public static bool operator !=(Score left, Score right)
	    {
	        return !left.Equals(right);
	    }
	}
}
