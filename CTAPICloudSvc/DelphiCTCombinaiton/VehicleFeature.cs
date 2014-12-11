using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Xml;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;
using DelphiCTCombinaiton.Models;
using System.Diagnostics;

namespace DelphiCTCombinaiton
{
    public class VehicleFeature
    {
        public async Task<List<Vehicle>> GetVehicleLstInfo(string address)
        {
            List<Vehicle> vehicleLst = new List<Vehicle>();
            var vehicleroot = new HttpItem() { RelatedAddress = address, RequestMethod = RequestMethod.Get, Authorization = TokenSeeker.CurrentToken.token_type + " " + TokenSeeker.CurrentToken.access_token, Content_Type = "application/hal+json" };
            var vehicleJson = await new HTTPHelper(vehicleroot).HttpHelperMethod().ContinueWith(vehicletask =>
                {
                    var result=vehicletask.Result;
                    return JsonHelper.JsonDeserialize<VehicleJson>(result);
                }
            );

            foreach (var item in vehicleJson._embedded.vehicle)
            {
                try
                {
                    vehicleLst.Add(item);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    continue;
                }
            }
                 return vehicleLst;

        }

        public void TestOtherInfor(Vehicle vehicle)
        {
            if (vehicle.Id == null)
                return;
            if (vehicle._links.self != null)
            {
                CommonTest.InvokeAddressWithGet(vehicle._links.self.href);
            }
            if (vehicle._links.device != null)
            {
                CommonTest.InvokeAddressWithGet(vehicle._links.device.href);
            }
            if (vehicle._links.vehicle_alert_configuration_list != null)
            {
                CommonTest.InvokeAddressWithGet(vehicle._links.vehicle_alert_configuration_list.href);
            }
            if (vehicle._links.vehicle_alert_list != null)
            {
                CommonTest.InvokeAddressWithGet(vehicle._links.vehicle_alert_list.href);
            }
            if (vehicle._links.vehicle_diagnostic_codes_list != null)
            {
                CommonTest.InvokeAddressWithGet(vehicle._links.vehicle_diagnostic_codes_list.href);
            }
            if (vehicle._links.vehicle_geo_fence_list != null)
            {
                CommonTest.InvokeAddressWithGet(vehicle._links.vehicle_geo_fence_list.href);
            }
            if (vehicle._links.vehicle_list != null)
            {
                CommonTest.InvokeAddressWithGet(vehicle._links.vehicle_list.href);
            }
            if (vehicle._links.vehicle_trip_list != null)
            {
                CommonTest.InvokeAddressWithGet(vehicle._links.vehicle_trip_list.href);
            }
        }

        public async Task<string> GetVehicleInfoString(string address)
        {
            var vehicleroot = new HttpItem() { RelatedAddress = address, RequestMethod = RequestMethod.Get, Authorization = TokenSeeker.CurrentToken.token_type + " " + TokenSeeker.CurrentToken.access_token, Content_Type = "application/hal+json" };
            var vehiclesJson = await new HTTPHelper(vehicleroot).HttpHelperMethod().ContinueWith(vehiclelisttask =>
            {
                return vehiclelisttask.Result;
            }
            );
            return vehiclesJson;
        }

        #region 
        /*
                 public async Task<List<Vehicle>> GetVehicleLstInfo(string address)
        {
            vehicleLst = new List<Vehicle>();
            var vehicleroot = new HttpItem() { RelatedAddress = address, RequestMethod = RequestMethod.Get, Authorization = TokenSeeker.CurrentToken.token_type + " " + TokenSeeker.CurrentToken.access_token, Content_Type = "application/hal+json" };
            var vehiclesJsonLst = new HTTPHelper(vehicleroot).HttpHelperMethod().ContinueWith(vehiclelisttask =>
                {
                    var mm = (JObject)JsonConvert.DeserializeObject(vehiclelisttask.Result);
                    return mm["_embedded"]["vehicle"].ToList();
                }
            );
            var t = await vehiclesJsonLst.ContinueWith(hello =>
             {
                 var tmp = hello.Result;
                 foreach (var item in tmp)
                 {
                     Trace.WriteLine(item["_links"]["vehicle-alert-configuration-list"]["href"]);

                     vehicleLst.Add(
                         new Vehicle() { Id = item["Id"].ToString(), Make = item["Make"].ToString(), Model = item["Model"].ToString(), Vin = item["Vin"].ToString(), Year = item["Year"].ToString(), _links = new VehicleLinks() { self = new Link() { href = item["_links"]["self"]["href"].ToString() } } }
    );
                     vehicleLst.Add(
    new Vehicle() { Id = item["Id"].ToString(), Make = item["Make"].ToString(), Model = item["Model"].ToString(), Vin = item["Vin"].ToString(), Year = item["Year"].ToString(), _links = new VehicleLinks() { self = new Link() { href = item["_links"]["vehicle-alert-configuration-list"]["href"].ToString() } } }
    );
                     vehicleLst.Add(
    new Vehicle() { Id = item["Id"].ToString(), Make = item["Make"].ToString(), Model = item["Model"].ToString(), Vin = item["Vin"].ToString(), Year = item["Year"].ToString(), _links = new VehicleLinks() { self = new Link() { href = item["_links"]["vehicle-alert-list"]["href"].ToString() } } }
    );
                     vehicleLst.Add(
    new Vehicle() { Id = item["Id"].ToString(), Make = item["Make"].ToString(), Model = item["Model"].ToString(), Vin = item["Vin"].ToString(), Year = item["Year"].ToString(), _links = new VehicleLinks() { self = new Link() { href = item["_links"]["vehicle-diagnostic-codes-list"]["href"].ToString() } } }
    );
                     vehicleLst.Add(
    new Vehicle() { Id = item["Id"].ToString(), Make = item["Make"].ToString(), Model = item["Model"].ToString(), Vin = item["Vin"].ToString(), Year = item["Year"].ToString(), _links = new VehicleLinks() { self = new Link() { href = item["_links"]["vehicle-geo-fence-list"]["href"].ToString() } } }
    );
                     vehicleLst.Add(
new Vehicle() { Id = item["Id"].ToString(), Make = item["Make"].ToString(), Model = item["Model"].ToString(), Vin = item["Vin"].ToString(), Year = item["Year"].ToString(), _links = new VehicleLinks() { self = new Link() { href = item["_links"]["vehicle-trip-list"]["href"].ToString() } } }
);

                 }
                 return vehicleLst;
             });
            return t;
        }
         */
        #endregion
    }
}
