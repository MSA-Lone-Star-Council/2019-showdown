using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Common.Models
{
    public class Location
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string Notes { get; set; }
    }
}

/*
{
  "id":1,
  "name":"Union Ballroom",
  "address":"Guadalupe street",
  "latitude":35.0,
  "longitude":40.0,
  "notes":"Upstairs"
}
*/
