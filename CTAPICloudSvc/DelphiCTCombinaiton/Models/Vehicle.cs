using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DelphiCTCombinaiton.Models
{
    public class Vehicle ///api/v1/vehicles/ec536015-e64a-4196-98fb-48e4a4199143
    {
        public VehicleLink _links { get; set; }

        public string FriendlyName { get; set; }
        public Guid Id { get; set; }
        public bool IsMMYEditable { get; set; }
        public bool IsVinEditable { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public VehiclkStatus Status { get; set; }
        public DateTimeOffset UpdatedOn { get; set; }
        public VehicleLocation VehicleLocation { get; set; }
        public string Vin { get; set; }
        public string Year { get; set; }

        //public string Description { get; set; }
        //public string Nickname { get; set; }
        //public int Speed { get; set; }
    }

    public class VehiclkStatus
    {
        public double BatteryVoltage { get; set; }
        public string EngineLightStatus { get; set; }
        public bool EngineOn { get; set; }
        public double FuelLevel { get; set; }
        public double Odometer { get; set; }
        public int Speed { get; set; }
        public int Temperature { get; set; }
    }

    public class VehicleLink
    {
        public Link device { get; set; }

        public Link self { get; set; }

        [JsonProperty(PropertyName = "vehicle-alert-configuration-list")]
        public Link vehicle_alert_configuration_list { get; set; }

        [JsonProperty(PropertyName = "vehicle-alert-list")]
        public Link vehicle_alert_list { get; set; }

        [JsonProperty(PropertyName = "vehicle-diagnostic-codes-list")]
        public Link vehicle_diagnostic_codes_list { get; set; }

        [JsonProperty(PropertyName = "vehicle-geo-fence-list")]
        public Link vehicle_geo_fence_list { get; set; }

        [JsonProperty(PropertyName = "vehicle-list")]
        public Link vehicle_list { get; set; }

        [JsonProperty(PropertyName = "vehicle-trip-list")]
        public Link vehicle_trip_list { get; set; }
    }

    public class VehicleJson
    {
        public string Page { get; set; }
        public VehicleEmbed _embedded { get; set; }
        public PageLink _links { get; set; }
    }

    public class VehicleEmbed
    {
        public  Vehicle[] vehicle { get; set; }
    }
}
