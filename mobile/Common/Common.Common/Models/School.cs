using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Common.Common.Models
{
    public struct School
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
		public string TopicId { get { return $"school_{Slug}"; } }

		[JsonProperty(PropertyName = "short_name")]
        public string ShortName;

        public string logo;

        public bool Equals(School other)
        {
            return string.Equals(ShortName, other.ShortName) &&
                   string.Equals(logo, other.logo) &&
                   string.Equals(Id, other.Id) &&
                   string.Equals(Name, other.Name) &&
                   string.Equals(Slug, other.Slug);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is School && Equals((School) obj);
        }

        public static bool operator ==(School left, School right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(School left, School right)
        {
            return !left.Equals(right);
        }

		public static List<School> FromJSONArray(string jsonString)
		{
			return JsonConvert.DeserializeObject<List<School>>(jsonString);
		}
    }
}