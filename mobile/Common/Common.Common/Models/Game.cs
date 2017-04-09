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

		public Event Event { get; set; }

		[JsonProperty(PropertyName= "away_team")]
		public School AwayTeam { get; set; }

		[JsonProperty(PropertyName="home_team")]
		public School HomeTeam { get; set; }

		public Score Score { get; set; }

	    [JsonProperty(PropertyName="in_progress")]
	    public bool InProgress { get; set; }

	    public static Game FromJSON(string jsonString)
	    {
	        return JsonConvert.DeserializeObject<Game>(jsonString);
	    }

	    public static List<Game> FromJSONArray(string jsonString)
	    {
	        return JsonConvert.DeserializeObject<List<Game>>(jsonString);
	    }

        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this);
        }

        public bool Equals(Game other)
	    {
	        return (
                ID == other.ID &&
                Title == other.Title &&
                Event == other.Event &&
                HomeTeam == other.HomeTeam &&
                AwayTeam == other.AwayTeam
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

		public string TopicId { get { return $"game_{ ID }"; } }
	}
}
