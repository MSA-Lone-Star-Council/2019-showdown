using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Admin.Common.API.Entities
{
    public struct Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
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