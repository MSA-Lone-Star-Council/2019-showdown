using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Common.Common.Models
{
	public struct Event
    {
        public string Id { get; set; }

		[JsonProperty(PropertyName = "start_time")]
		public DateTimeOffset StartTime { get; set; }

		[JsonProperty(PropertyName = "end_time")]
		public DateTimeOffset EndTime { get; set; }

        public string Title { get; set; }

		public string Audience { get; set; }

        public string Description { get; set; }

        public Location Location { get; set; }

		public static Event FromJSON(string jsonString)
		{
			return JsonConvert.DeserializeObject<Event>(jsonString);
		}

		public static List<Event> FromJSONArray(string jsonString)
		{
			return JsonConvert.DeserializeObject<List<Event>>(jsonString);
		}

		public static bool operator ==(Event one, Event two)
		{
			return (
				one.Id == two.Id &&
				one.StartTime == two.StartTime &&
				one.EndTime== two.EndTime &&
				one.Description == two.Description &&
				one.Location == two.Location
			);
		}

		public static bool operator !=(Event one, Event two)
		{
			return !(one == two);
		}
    }
}
