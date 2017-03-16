using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Common.Models
{
    class Event
    {
        public string Id { get; set; }

        //TODO: Make this a DateTime Object that's compatible cross-platform
        public string StartTime { get; set; }

        //TODO: Make this a DateTime Object that's compatible cross-platform
        public string EndTime { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }
    }
}
