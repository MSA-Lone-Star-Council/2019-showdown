using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Common.Models;

namespace Client.Common
{
    public interface IScheduleView
    {
        List<Event> Events { set; }

        void ShowMessage(string message);

        void OpenEvent(Event row);

		Task ScheduleReminder(Event eventToRemind);
    }
}
