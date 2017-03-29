using System;
using NUnit.Framework;

using Common.Common;
using Common.Common.Models;

namespace Common.Test
{
	[TestFixture]
	public class ClientTests
	{
		[Test]
		public async void TestGetSchedule()
		{
			var client = new ShowdownRESTClient();
			var result = await client.GetScheduleAsync();

			Assert.IsNotEmpty(result);
		}

		[Test]
		public async void TestGetLocation()
		{
			var client = new ShowdownRESTClient();
			var result = await client.GetLocationInformation("1");

			Assert.True(result != default(Location));
		}


        [Test]
		public async void TestGetEventGames()
		{
			var client = new ShowdownRESTClient();
			var result = await client.GetEventGames("1");

			Assert.IsNotEmpty(result);
		}

        [Test]
		public async void TestGetGameHistory()
		{
			var client = new ShowdownRESTClient();
			var result = await client.GetScoreHistory("d1cd7cd7-e037-43c3-8845-a6f36de25c59");

			Assert.IsNotEmpty(result);
		}

	    [Test]
		public async void TestGetAnnouncments()
		{
			var client = new ShowdownRESTClient();
			var result = await client.GetAnnouncements();

			Assert.IsNotEmpty(result);
		}

	    [Test]
	    public async void TestLogin()
	    {
	        var client = new ShowdownRESTClient();
	        var result = await client.GetToken("EAAIwmUsfMZAcBAHbCYPsZCsp06Yec66vToS12AOhF8fZCOevvjg3zHeZBZAEAxj7OMPmkW540XjNsyfI069dTK0E3ZCGR6dCG9fD3tx2i3vjaWgBLZAYL5tvXIlBuPZBsKZAl8n8tscacwquS7NIomcufRijiV5AO11CKBtzfa9E1kV4HBLQOnZAuTEZCLUJLKBHri2VwOIZBfTUVjR6th0JXwR8JXcCIkzqO8AZD");

	        Assert.IsFalse(string.IsNullOrEmpty(result));
	    }
	}
}
