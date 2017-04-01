using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Common.Common.Models
{
    public struct Announcement
    {
		[JsonProperty(PropertyName = ("id"), NullValueHandling = NullValueHandling.Ignore)]
		public string Id { get; set; }

		[JsonProperty(PropertyName=("title"))]
        public string Title { get; set; }

		[JsonProperty(PropertyName=("body"))]
        public string Body { get; set; }

		[JsonProperty(PropertyName=("time"), NullValueHandling=NullValueHandling.Ignore)]
        public DateTimeOffset? Time { get; set; }

        public static Announcement FromJSON(string jsonString)
		{
			return JsonConvert.DeserializeObject<Announcement>(jsonString);
		}

		public static List<Announcement> FromJSONArray(string jsonString)
		{
			return JsonConvert.DeserializeObject<List<Announcement>>(jsonString);
		}
    }
}