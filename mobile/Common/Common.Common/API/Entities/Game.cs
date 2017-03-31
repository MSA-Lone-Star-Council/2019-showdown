using System;
using System.Collections.Generic;
using Common.Common.Models;
using Newtonsoft.Json;

namespace Admin.Common.API.Entities
{
	public struct Game
	{
		[JsonProperty(PropertyName = "id")]
		public string Id { get; set; }

		[JsonProperty(PropertyName = "title")]
		public string Title { get; set; }

		[JsonProperty(PropertyName = "event")]
		public int EventId { get; set; }

		[JsonProperty(PropertyName = "scorekeeper")]
		public string ScorekeeperId { get; set; }

		[JsonProperty(PropertyName = "away_team")]
		public string AwayTeamId { get; set; }

		[JsonProperty(PropertyName = "home_team")]
		public string HomeTeamId { get; set; }

		[JsonProperty(PropertyName = "in_progress")]
		public bool InProgress { get; set; }

		public static List<Game> FromJSONArray(string jsonString)
		{
			return JsonConvert.DeserializeObject<List<Game>>(jsonString);
		}
	}
}
