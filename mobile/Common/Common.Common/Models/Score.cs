using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Common.Common.Models
{
	public struct Score
	{
		[JsonProperty(PropertyName = "away_points")]
		public double AwayPoints { get; set; }

		[JsonProperty(PropertyName = "home_points")]
		public double HomePoints { get; set; }

	    public DateTimeOffset Time { get; set; }

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
	        return AwayPoints.Equals(other.AwayPoints) && HomePoints.Equals(other.HomePoints) && Time.Equals(other.Time);
	    }

	    public override bool Equals(object obj)
	    {
	        if (ReferenceEquals(null, obj)) return false;
	        return obj is Score && Equals((Score) obj);
	    }

	    public override int GetHashCode()
	    {
	        unchecked
	        {
	            var hashCode = AwayPoints.GetHashCode();
	            hashCode = (hashCode * 397) ^ HomePoints.GetHashCode();
	            hashCode = (hashCode * 397) ^ Time.GetHashCode();
	            return hashCode;
	        }
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
