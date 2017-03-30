using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Admin.Common.API.Entities
{
    public struct Location
    {
		[JsonProperty(PropertyName="id", NullValueHandling=NullValueHandling.Ignore)]
        public int? Id { get; set; }

		[JsonProperty(PropertyName="name")]
        public string Name { get; set; }

		[JsonProperty(PropertyName="address")]
        public string Address { get; set; }

		[JsonProperty(PropertyName = "latitude")]
        public float Latitude { get; set; }

		[JsonProperty(PropertyName = "longitude")]
        public float Longitude { get; set; }

		[JsonProperty(PropertyName = "notes")]
        public string Notes { get; set; }

        public static Location FromJSON(string jsonString)
        {
            return JsonConvert.DeserializeObject<Location>(jsonString);
        }

        public static List<Location> FromJSONArray(string jsonString)
        {
            return JsonConvert.DeserializeObject<List<Location>>(jsonString);
        }
    }
}