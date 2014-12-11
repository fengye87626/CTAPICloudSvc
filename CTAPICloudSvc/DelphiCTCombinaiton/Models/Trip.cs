using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DelphiCTCombinaiton.Models
{
    public class Trip
    {
        public TripLinks _links{get;set;}
        public double Distance { get; set; }
        public Guid Id { get; set; }
        public double IdleTime { get; set; }
        public DateTimeOffset StartDateTime { get; set; }
        public DateTimeOffset EndDateTime { get; set; }
        public VehicleLocation[] TripRoute { get; set; }
    }

#warning https://jira.lixar.net/browse/DELAPI-930
    //public class TripRoute
    //{
    //    public Location Location { get; set; }
    //    public DateTimeOffset Time { get; set; }
    //}

    public class TripLinks
    {
        public Link self { get; set; }
        [JsonProperty(PropertyName = "vehicle-trip-list")]
        public Link vehicle_trip_list { get; set; }
    }

    public class TripsJson //api/v1/vehicles/806055f7-c916-441a-9075-612f77a53e3f/trips
    {
        public int Page { get; set; }
        public TripsPageLink _links { get; set; }
        public Trips_embedded _embedded { get; set; }
    }

    public class TripsPageLink:PageLink
    {
        public Link vehicle { get; set; }
    }
    public class Trips_embedded
    {
        Trip[] trip { get; set; }
    }

}
