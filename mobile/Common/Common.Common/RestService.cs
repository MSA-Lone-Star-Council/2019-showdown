using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Common.Common.Models;

namespace Common.Common
{
    class RestService
    {
        HttpClient client;

        public RestService()
        {
            client = new HttpClient()
            {
                MaxResponseContentBufferSize = 256000
            };
        }

        public async Task<List<Event>> RefreshScheduleAsync()
        {
            var Events = new List<Event>();

            //Move to a "Constants" file once finalized
            string RestUrl = "http://developer.xamarin.com:8081/api/todoitems{0}"; 

            var uri = new Uri(string.Format(RestUrl, string.Empty));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Events = JsonConvert.DeserializeObject<List<Event>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return Events;
        }

    }
}
