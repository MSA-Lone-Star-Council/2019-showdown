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

        private async Task<List<Event>> UpdateFromServer()
        {
            return ShowdownRESTClient.MakeFakeData();
        }
}
