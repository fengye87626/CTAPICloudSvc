using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DelphiCTCombinaiton.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;


namespace DelphiCTCombinaiton
{
    public class DeviceFeature
    {
        public async Task<List<Device>> GetDeviceLstInfo(string address) //= @"/api/v1/devices"
        {
            List<Device> DeviceLst = new List<Device>();
            var root = new HttpItem() { RelatedAddress = address, RequestMethod = RequestMethod.Get, Authorization = TokenSeeker.CurrentToken.token_type + " " + TokenSeeker.CurrentToken.access_token, Content_Type = "application/hal+json" };
            var DevicesJson = await new HTTPHelper(root).HttpHelperMethod().ContinueWith(Devicetask =>
            {
                var result = Devicetask.Result;
                return JsonHelper.JsonDeserialize<DeviceJson>(result);
            });

            foreach (var item in DevicesJson._embedded.device)
            {
                try
                {
                    DeviceLst.Add(item);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    continue;
                }
            }
            return DeviceLst;
        }

        public async Task<string> GetDeviceInfoString(string address) //= @"/api/v1/devices"
        {
            var root = new HttpItem() { RelatedAddress = address, RequestMethod = RequestMethod.Get, Authorization = TokenSeeker.CurrentToken.token_type + " " + TokenSeeker.CurrentToken.access_token, Content_Type = "application/hal+json" };
            var DeviceInfoJson = await new HTTPHelper(root).HttpHelperMethod().ContinueWith(Devicetask =>
            {
                return Devicetask.Result;

            });
            return DeviceInfoJson;
        }

        public void GetDeviceByID(Device device)
        {
            if (device == null)
                return;

            var httpitem = new HttpItem() { RelatedAddress = string.Format("/api/v1/devices/{0}", device.Id), RequestMethod = RequestMethod.Get, Authorization = TokenSeeker.CurrentToken.token_type + " " + TokenSeeker.CurrentToken.access_token, Content_Type = "application/hal+json" };
            new HTTPHelper(httpitem).HttpHelperMethod().GetAwaiter().GetResult();
            if (device._links.vehicle != null)
            {
               CommonTest.InvokeAddressWithGet(device._links.vehicle.href);
            }
            if (device._links.customer != null)
            {
                CommonTest.InvokeAddressWithGet(device._links.customer.href);
            }
        }

        //TODO: Delete a Device


        public const string Device_ID = @"/api/v1/devices/{0}";
        public const string Device_Guid = @"/api/v1/devices/find/guid/{0}";
        public const string Device_Regkey = @"/api/v1/devices/find/regkey/{0}";
        public const string Device_CarrID = @"/api/v1/devices/find/carrierid/{0}";
        /// <summary>
        ///Get a Device	GET	device	/api/v1/devices/{ID}        /api/v1/devices/find/guid/{id}
        ///Get a Device by registration number	GET	device	/api/v1/devices/find/regkey/{REGKEY}
        ///Get a Device by carrier id	GET	device	/api/v1/devices/find/carrierid/{CARRIERID}
        /// </summary>
        /// <param name="tmpAddress"></param>
        /// <param name="arr"></param>
        /// <returns></returns>
        public async Task<Device> GetDevice(string tmpAddress, params string[] arr)
        {
            if (arr == null)
                return null;
            string address=string.Format(tmpAddress,arr);
            var httpitem = new HttpItem() { RelatedAddress = address, RequestMethod = RequestMethod.Get, Authorization = TokenSeeker.CurrentToken.token_type + " " + TokenSeeker.CurrentToken.access_token, Content_Type = "application/hal+json" };
           string devicestr= await new HTTPHelper(httpitem).HttpHelperMethod();
           if (devicestr==null)
               return null;
           return JsonHelper.JsonDeserialize<Device>(devicestr);
        }

        #region
        /*
        public async Task<List<Device>> GetDeviceLstInfo(string address) //= @"/api/v1/devices"
        {
            List<Device> DeviceLst = new List<Device>();
            var root = new HttpItem() { RelatedAddress = address, RequestMethod = RequestMethod.Get, Authorization = TokenSeeker.CurrentToken.token_type + " " + TokenSeeker.CurrentToken.access_token, Content_Type = "application/hal+json" };
            var DevicesJson = await new HTTPHelper(root).HttpHelperMethod().ContinueWith(Devicetask =>
            {
                var mm = (JObject)JsonConvert.DeserializeObject(Devicetask.Result);

                return mm["_embedded"]["device"].ToList();
            });

            foreach (var item in DevicesJson)
            {
                Trace.WriteLine(item);
                try
                {
                    string VehicleLink = string.Empty;
                    try
                    {
                        VehicleLink = item["_links"]["vehicle"]["href"].ToString();
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(ex.InnerException.Message);
                        continue;
                    }
                    DeviceLst.Add(new Device() { Id = item["Id"].ToString(), DeviceType = JsonHelper.JsonDeserialize<DeviceType>(item["DeviceType"].ToString()), RegistrationNumber = item["RegistrationNumber"].ToString(), ByteId = item["ByteId"].ToString(), LabelId = item["LabelId"].ToString(), LabelIdType = item["LabelIdType"].ToString(), LastConnection = item["LastConnection"].ToString(), _links = new DeviceLink() { vehicle = new Link() { href = VehicleLink } } });
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    continue;
                }
            }
            return DeviceLst;
        }
        */
        #endregion
    }
}
