using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Common;
using Common.Common.Models;


namespace Client.Common
{
    public class SchedulePresenter : Presenter<IScheduleView>
    {
        private readonly ShowdownRESTClient _client;
		SubscriptionManager manager;

		List<Event> events;

		public SchedulePresenter(ShowdownRESTClient client, SubscriptionManager manager = null)
        {
            _client = client;
			this.manager = manager;
        }

        public async Task OnBegin()
        {
            if (Connectivity.IsConnected()) 
            {
                await UpdateFromServer(); 
            } else 
            {
                events = new List<Event>(); //Empty List
                View.ShowMessage("Not Connected to the internet");
            }

        }

        public async Task OnTick()
        {
            await UpdateFromServer();
        }

        public async Task OnStar(Event e)
        {
			if(manager != null) await manager.ToggleSubscription(e.TopicId);
			if (View != null)
			{
				View.Events = events; // force a refresh...
				await View.ScheduleReminder(e);
			}
        }

        public void OnClickRow(Event row)
        {
            View.OpenEvent(row);
        }

		private async Task UpdateFromServer()
		{
                try 
                {
                    events = await _client.GetScheduleAsync();
                } catch(Exception e) 
                {
                    View.ShowMessage("Oops! Something went wrong");
                    Console.WriteLine(e.StackTrace);
                    events = new List<Event>();
                }
			

			if (View != null)
				View.Events = events;
		}

		public bool IsSubscribed(Event item)
		{
			if (manager == null) return false;

			return manager[item.TopicId];
		}
	}
}
