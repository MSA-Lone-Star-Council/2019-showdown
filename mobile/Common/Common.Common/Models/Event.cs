using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Common.Models
{
	public struct Event
    {
        public string Id { get; set; }

		public DateTimeOffset StartTime { get; set; }

		public DateTimeOffset EndTime { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Location Location { get; set; }
    }
}
