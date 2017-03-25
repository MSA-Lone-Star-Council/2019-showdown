using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Common.Models;

namespace Client.Common
{
    interface IScheduleView
    {
        List<Event> events { get; }
    }
}
