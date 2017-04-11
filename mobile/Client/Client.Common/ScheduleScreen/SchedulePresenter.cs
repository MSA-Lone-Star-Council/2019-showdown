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
            await UpdateFromServer();
        }

        public async Task OnTick()
        {
            await UpdateFromServer();
        }

        public void OnStar(Event e)
        {
			if(manager != null) manager.ToggleSubscription(e.TopicId);
			if (View != null)
			{
				View.Events = events; // force a refresh...
				View.ScheduleReminder(e);
			}
        }

        public void OnClickRow(Event row)
        {
            View.OpenEvent(row);
        }

		private async Task UpdateFromServer()
		{
			events = await _client.GetScheduleAsync();

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
