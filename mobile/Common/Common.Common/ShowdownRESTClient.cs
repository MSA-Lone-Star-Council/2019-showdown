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

		public async Task<List<Event>> GetSchedule()
		{
			var jsonString = await RequestAsync("/events/schedule");
			return Event.FromJSONArray(jsonString);
		}

		public async Task<Location> GetLocationInformation(string id)
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

        public async Task<List<Game>> GetEventGames(string eventId)
        {
            var path = $"/events/{eventId}/games";
            var jsonString = await RequestAsync(path);
            return Game.FromJSONArray(jsonString);
        }

        public async Task<List<ScoreRecord>> GetScoreHistory(String gameId)
        {
            var path = $"/scores/games/{gameId}/scores";
            var jsonString = await RequestAsync(path);
            return ScoreRecord.FromJSONArray(jsonString);
        }

		private async Task<string> RequestAsync(string path)
		{
			var url = $"{Secrets.BACKEND_URL}{path}.json";
			var response = await client.GetAsync(url);
			return await response.Content.ReadAsStringAsync();
		}
    }
}
