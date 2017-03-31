using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Admin.Common.API.Entities;
using Common.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ClientModels = Common.Common.Models;

namespace Admin.Common.API
{
    public class AdminRESTClient
    {
        HttpClient client;

        public String Token { get; set; }

		public AdminRESTClient()
		{
			client = new HttpClient();
		}

        public async Task<string> GetToken(string facebookAccessToken)
        {
			string jsonString = JsonConvert.SerializeObject(new { facebookAccessToken = facebookAccessToken }, Formatting.None);
            string response = await PostAsync("/accounts/login", jsonString, authenticated: false);

            var data = JObject.Parse(response);
			return ((string)data["token"]);
        }

		public async Task<List<ClientModels.School>> GetSchools()
		{
			var jsonString = await RequestAsync("/core/schools");
			return ClientModels.School.FromJSONArray(jsonString);
		}

		public async Task<Event> GetEvent(int id)
		{
			var path = $"/admin/events/{id}";
			var jsonString = await RequestAsync(path);
			return Event.FromJSON(jsonString);
		}

		public async Task<List<Event>> GetEvents()
		{
			var jsonString = await RequestAsync("/admin/events");
			return Event.FromJSONArray(jsonString);
		}

		public async Task<Event> SaveEvent(Event e)
		{
			var jsonString = "";
			if (e.Id == null)
			{
				var path = $"/admin/events";
				jsonString = await PostAsync(path, JsonConvert.SerializeObject(e));
			}
			else
			{
				var path = $"/admin/events/{e.Id}";
				jsonString = await PutAsync(path, JsonConvert.SerializeObject(e));
			}

			return Event.FromJSON(jsonString);
		}

		public async Task<Location> GetLocation(int id)
		{
			var path = $"/admin/locations/{id}";
			var jsonString = await RequestAsync(path);
			return Location.FromJSON(jsonString);
		}

		public async Task<List<Location>> GetLocations()
		{
			var path = $"/admin/locations";
			var jsonString = await RequestAsync(path);
			return Location.FromJSONArray(jsonString);
		}

		public async Task<List<Game>> GetGames()
		{
			var jsonString = await RequestAsync("/admin/games");
			return Game.FromJSONArray(jsonString);
		}

        private HttpRequestMessage BuildRequest(string path, string jsonBody, bool authenticated)
        {
            var url = $"{Secrets.BACKEND_URL}{path}.json";
            var request = new HttpRequestMessage() {RequestUri = new Uri(url)};
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (jsonBody != null) request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            if (authenticated) request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);

            return request;
        }

		private async Task<string> RequestAsync(string path, bool authenticated = true)
		{
		    var request = BuildRequest(path, null, authenticated);
		    request.Method = HttpMethod.Get;
		    var response = await client.SendAsync(request);
			return await response.Content.ReadAsStringAsync();
		}

        private async Task<string> PostAsync(string path, string jsonString, bool authenticated = true)
        {
		    var request = BuildRequest(path, jsonString, authenticated);
		    request.Method = HttpMethod.Post;
		    var response = await client.SendAsync(request);
			return await response.Content.ReadAsStringAsync();
        }

        private async Task<string> PutAsync(string path, string jsonString, bool authenticated = true)
        {
		    var request = BuildRequest(path, jsonString, authenticated);
		    request.Method = HttpMethod.Put;
		    var response = await client.SendAsync(request);
			return await response.Content.ReadAsStringAsync();
        }
    }
}