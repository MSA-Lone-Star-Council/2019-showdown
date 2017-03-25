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

        public SchedulePresenter(ShowdownRESTClient client)
        {
            _client = client;
        }

        public async Task OnBegin()
        {
            await UpdateFromServer();
        }

        public async Task OnTick()
        {
            await UpdateFromServer();
        }

        public async Task OnStar()
        {
            View.ShowMessage("Subscribed for notifications for this game!");
            // TODO: What the message says
        }

        public void OnClickRow(Event row)
        {
            View.OpenEvent(row);
        }

        private async Task UpdateFromServer()
        {
            var gamesFromServer = await GetAllEvents();

            if (View != null)
                View.Events = gamesFromServer.OrderByDescending(g => g.StartTime).ToList();
        }

        private async Task<List<Event>> GetAllEvents()
        {
            return await _client.GetSchedule();
        }
    }
}
