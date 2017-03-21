using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Common.Common.Models;

namespace Common.Common
{
    public class ShowdownRESTClient
    {
        HttpClient client;
        static string RestUrl;


        public ShowdownRESTClient()
        {
            client = new HttpClient()
            {
                MaxResponseContentBufferSize = 256000
            };
            RestUrl = Secrets.BACKEND_URL;
        }

        //Get a list of Schedule Events
        public async Task<List<Event>> RefreshSchedule()
        {
            var Events = new List<Event>();

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

        //Add a new Event
        public async Task AddEvent(Event item, bool isNewItem = false)
        {
            var uri = new Uri(string.Format(RestUrl, item.Id));

            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    response = await client.PostAsync(uri, content);
                }
                else
                {
                    response = await client.PutAsync(uri, content);
                }

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"				TodoItem successfully saved.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
        }

        //Delete an event
        public async Task DeleteEvent(Event item)   //"Event item", or "string id"?
        {
            var uri = new Uri(string.Format(RestUrl, item.Id));
            try
            {
                var response = await client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"				TodoItem successfully deleted.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
        }

        public static List<Event> MakeFakeData()
        {
            var event1 = new Event
            {
                Id = "0",
                StartTime = "900",
                EndTime = "1100",
                Description = "Listen to Dudes sing",
                Title = "Brothers Nasheed"
            };
            var event2 = new Event
            {
                Id = "1",
                StartTime = "900",
                EndTime = "1100",
                Description = "Listen to gals sing",
                Title = "Sisters Nasheed"
            };
            List<Event> events = new List<Event>();
            for (int i = 0; i < 5; i++)
            {
                if (i % 2 != 0) { event1.Id = i.ToString(); }
                else            { event2.Id = i.ToString(); }

                events.Add(event1);
                events.Add(event2);
            }
            return events;
        }
    }
}
