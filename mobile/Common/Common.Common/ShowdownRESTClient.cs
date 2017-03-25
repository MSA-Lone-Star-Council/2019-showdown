using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Common.Common.Models;

namespace Common.Common
{
    public class ShowdownRESTClient
    {
        HttpClient client;

		public ShowdownRESTClient()
		{
			client = new HttpClient();
		}

		public async Task<List<Event>> GetScheduleAsync()
		{
			var jsonString = await RequestAsync("/events/schedule");
			return Event.FromJSONArray(jsonString);
		}

		public async Task<Location> GetLocationInformation(int id)
		{
			var path = $"/events/locations/{id}";
			var jsonString = await RequestAsync(path);
			return Location.FromJSON(jsonString);
		}

        public async Task<List<Game>> GetAllGames()
        {
            var jsonString = await RequestAsync("/scores/games");
            return Game.FromJSONArray(jsonString);
        }

        public async Task<List<Game>> GetEventGames(int eventId)
        {
            var path = $"/events/{eventId}/games";
            var jsonString = await RequestAsync(path);
            return Game.FromJSONArray(jsonString);
        }

		private async Task<string> RequestAsync(string path)
		{
			var url = $"{Secrets.BACKEND_URL}{path}.json";
			var response = await client.GetAsync(url);
			return await response.Content.ReadAsStringAsync();
		}
    }
}
