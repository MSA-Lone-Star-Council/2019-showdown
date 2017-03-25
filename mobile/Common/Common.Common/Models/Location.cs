using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Common.Common.Models
{
    public struct Location
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Notes { get; set; }

		public string ToJSON()
		{
			return JsonConvert.SerializeObject(this);
		}

		public static Location FromJSON(string jsonString)
		{
			return JsonConvert.DeserializeObject<Location>(jsonString);
		}

		public static bool operator ==(Location one, Location two)
		{
			return (
				one.Id == two.Id &&
				one.Name == two.Name &&
				one.Address == two.Address &&
				one.Latitude == two.Latitude &&
				one.Longitude == two.Longitude &&
				one.Notes == two.Notes
			);
		}

		public static bool operator !=(Location one, Location two)
		{
			return !(one == two);
		}
    }
}
