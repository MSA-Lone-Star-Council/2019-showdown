using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Common.Common.Models
{
    public struct Announcement
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTimeOffset Time { get; set; }

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