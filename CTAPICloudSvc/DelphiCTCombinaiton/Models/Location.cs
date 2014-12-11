using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DelphiCTCombinaiton.Models
{
    public class Location
    {
       public double Latitude {get;set;}//Latitude=43.806914
       public double Longitude { get; set; } //Longitude=-79.2805458
    }

    public class VehicleLocation
    {
        [JsonProperty(Required=Required.Default)]
        public double Heading { get; set; }
        public Location Location { get; set; }
        public DateTimeOffset UpdatedOn { get; set; }
    }
}
