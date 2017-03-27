using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Admin.Common.API.Entities
{
    public struct Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Audience { get; set; }
        public string Description { get; set; }

        [JsonProperty(PropertyName = "start_time")]
        public DateTimeOffset StartTime { get; set; }

        [JsonProperty(PropertyName = "end_time")]
        public DateTimeOffset EndTime { get; set; }

        [JsonProperty(PropertyName = "location")]
        public int LocationId { get; set; }

        public static Event FromJSON(string jsonString)
        {
            return JsonConvert.DeserializeObject<Event>(jsonString);
        }

        public static List<Event> FromJSONArray(string jsonString)
        {
            return JsonConvert.DeserializeObject<List<Event>>(jsonString);
        }
    }
}