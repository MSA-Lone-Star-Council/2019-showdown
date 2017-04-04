using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Common.Common
{
	public struct User
	{
		public string Id { get; set; }
		public string Name { get; set; }

		public static List<User> FromJSONArray(string jsonString)
		{
			return JsonConvert.DeserializeObject<List<User>>(jsonString);
		}
	}
}
