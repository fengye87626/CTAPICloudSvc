using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DelphiCTCombinaiton.Models;
using System.Diagnostics;

namespace DelphiCTCombinaiton
{
    public class CTMsgHelper
    {
        DeviceFeature dvcFeature = new DeviceFeature();

        VehicleFeature vehicleFeature = new VehicleFeature();

        public Dictionary<Device, Vehicle> GetAvailableVehicleInfoList(List<Device> devicelst) //List<CTGPSlocationInfo>
        {
            Dictionary<Device, Vehicle> ctgpsLst = new Dictionary<Device, Vehicle>();
            var sync = new object();

            Parallel.ForEach(devicelst,  (device) =>
                {
                    try
                    {
                        if (device != null)
                        {
                            //Trace.WriteLine(device._links.vehicle);
                            if (device._links != null && device._links.vehicle != null)
                            {
                                var vestr =  vehicleFeature.GetVehicleInfoString(device._links.vehicle.href).Result;
                                if (!string.IsNullOrEmpty(vestr))
                                {
                                    var vehicle = JsonHelper.JsonDeserialize<Vehicle>(vestr);
                                    try
                                    {
                                        if (vehicle != null)
                                        {
                                            lock (sync)
                                            {
                                                ctgpsLst.Add(device, vehicle);
                                            }
                                        }
                                        //(new T() { deviceId = device.ByteId, authToken = CTTokenSeeker.CurrentToken.authToken, posLatitude = vehicle.VehicleLocation.Location.Latitude.ToString("0.0000"), posLongitude = vehicle.VehicleLocation.Location.Longitude.ToString("0.0000"), posAltitude = (int)vehicle.VehicleLocation.Heading, posTime = vehicle.UpdatedOn.DateTime.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss"), alertStatus=0});
                                    }
                                    catch (Exception ex)
                                    {
                                        Trace.WriteLine(ex.StackTrace);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(ex.StackTrace);
                    }
                }
                );

            return ctgpsLst;
        }

        public List<Device> GetAvailableDeviceList(List<string> deviceIDLst)
        {
            List<Device> deviceLst = new List<Device>();
            var sync = new object();
            Parallel.ForEach(deviceIDLst,  (item) =>
                 {
                     try
                     {
                         var device =  dvcFeature.GetDevice(DeviceFeature.Device_CarrID, item).Result;
                         if (device != null)
                         {
                             lock (sync)
                             {
                                 deviceLst.Add(device);
                             }
                         }
                     }
                     catch (Exception ex)
                     {
                         Trace.WriteLine(ex.StackTrace);
                     }
                 }
                 );

            return deviceLst;
        }

    }
}
