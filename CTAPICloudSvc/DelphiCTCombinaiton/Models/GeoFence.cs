using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DelphiCTCombinaiton.Models
{
    public class GeoFence
    {   
        public GeoFenceLinks _links { get; set; }
        public int Index { get; set; }
        public string Id { get; set; }
        public Location[] Coordinates { get; set; } //FixType
        public string CoordinateType { get; set; }
        public bool Enabled { get; set; }
        public string GeoFenceType { get; set; }

        public string Name { get; set; }
        public int Radius { get; set; }
    }

    public class PostOrPutGeofence
    {
        public Location[] Coordinates { get; set; }
        public string CoordinateType { get; set; }
        public bool Enabled { get; set; }
        public string GeoFenceType { get; set; }

        public string Name { get; set; }
        public int Radius { get; set; }
    }

    public enum GeoFenceType
    {
        CIRCLE,
        POLYGON
    }


    public class GeoFenceLinks
    {
        public Link self { get; set; }

        public Link vehicle { get; set; }

        [JsonProperty(PropertyName = "vehicle-geo-fence-list")]
        public Link vehicle_geo_fence_list { get; set; }
    }


    public class GeoFenceJson
    {
        public int Page { get; set; }

        public PageLink _links { get; set; }

        public GeoFenceEmbeded _embedded { get; set; } 
    }



    public class GeoFenceEmbeded
    {
       [JsonProperty(PropertyName = "geo-fence")]
        public List<GeoFence> geo_fence {get;set;}
    }

}
