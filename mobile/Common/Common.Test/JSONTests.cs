using NUnit.Framework;
using System;
using System.Collections.Generic;
using Common.Common.Models;

namespace Common.Test
{
	[TestFixture]
	public class JSONTests
	{

		[Test]
		public void TestLocationDeserialization()
		{
			string jsonString = @"
				{
					""id"": 1,
					""name"": ""Union Ballroom"",
					""latitude"": 35.0,
					""longitude"": 40.0,
					""notes"": ""Upstairs""
				}
			";

			Location expected = new Location()
			{
				Id = "1",
				Name = "Union Ballroom",
				Latitude = 35.0,
				Longitude = 40.0,
				Notes = "Upstairs"
			};

			Location actual = Location.FromJSON(jsonString);

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void TestBriefLocationDeserialization()
		{
			string jsonString = @"
				{
					""id"": 1,
					""name"": ""Union Ballroom"",
				}
			";

			Location expected = new Location()
			{
				Id = "1",
				Name = "Union Ballroom",
				Latitude = 0,
				Longitude = 0,
				Notes = null
			};

			Location actual = Location.FromJSON(jsonString);

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void TestEventDeserialization()
		{
			string jsonString = @"
			{
			    ""id"":1,
			    ""location"":{
			      ""id"":1,
			      ""name"":""Texas Union""
			    },
			    ""title"":""Rollcall"",
			    ""audience"":""general"",
			    ""start_time"":""2017-03-17T03:21:10.000000Z"",
			    ""end_time"":""2017-03-17T03:21:10.00000Z"",
			    ""description"":""Show your cheer!""
			  }
			";

			Event expected = new Event()
			{
				Id = "1",
				Location = new Location() { Id = "1", Name = "Texas Union" },
				Title = "Rollcall",
				Audience = "general",
				StartTime = new DateTimeOffset(2017, 3, 17, 3, 21, 10, TimeSpan.Zero),
				EndTime = new DateTimeOffset(2017, 3, 17, 3, 21, 10, TimeSpan.Zero),
				Description = "Show your cheer!"
			};

			Event actual = Event.FromJSON(jsonString);

			Assert.AreEqual(expected, actual);
		}

	}
}
