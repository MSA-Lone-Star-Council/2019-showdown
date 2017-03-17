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

        public Location Location { get; set; }
    }
}

/*
For URL /events/events.json

[
  {
    "id":1,
    "location":{
      "id":1,
      "name":"Texas Union"
    },
    "title":"Rollcall",
    "audience":"general",
    "start_time":"2017-03-17T03:21:10.013312Z",
    "end_time":"2017-03-17T03:21:10.013326Z",
    "description":"Show your cheer!"
  },
  {
    "id":2,
    "location":{
      "id":1,
      "name":"Texas Union"
    },
    "title":"Rollcall",
    "audience":"general",
    "start_time":"2017-03-17T03:21:10.013312Z",
    "end_time":"2017-03-17T03:21:10.013326Z",
    "description":"Show your cheer!"
  }
]
*/