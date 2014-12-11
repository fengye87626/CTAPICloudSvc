using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DelphiCTCombinaiton.Models;
using System.Diagnostics;

namespace DelphiCTCombinaiton
{
   public class CTGANInfoFeature
    {
        public static void PostCTVehicleCANInfoList(Dictionary<Device, Vehicle> devicevehicle)
        {
            List<CTCANMsgInfo> CANInfoLst = new List<CTCANMsgInfo>();
            var sync = new object();
            try
            {
                Parallel.ForEach(devicevehicle, item => {
                    lock(sync)
                    {
                        CANInfoLst.Add(new CTCANMsgInfo() { deviceId = item.Key.CarrierId, authToken = CTTokenSeeker.CurrentToken.authToken, posAltitude = (int)item.Value.VehicleLocation.Heading, posLatitude = item.Value.VehicleLocation.Location.Latitude.ToString("0.0000"), posLongitude = item.Value.VehicleLocation.Location.Longitude.ToString("0.0000"), vin = item.Value.Vin, vehicleSpeed = item.Value.Status.Speed, engineRpm = 4000, distanceTotal = (int)item.Value.Status.Odometer, fuelPressure = item.Value.Status.FuelLevel.ToString(), engineCoolliquidTemp = item.Value.Status.Temperature, storageBatteryVoltage = item.Value.Status.BatteryVoltage, posTime = item.Value.UpdatedOn.DateTime.ToLocalTime().ToString("yyyy-MM-dd hh:mm:ss") });
                    }
                    }
                    );
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.StackTrace);
            }
         // PostCTInfo(CANInfoLst);

          CANInfoLst.PostContent2CT<CTCANMsgInfo>(t => {
              if (t == null)
                  return;
             CTHttpHelper.PostContent<CTCANMsgInfo>(t).GetAwaiter();
              }
                  );
        }

       /*
        public static void PostCTInfo(List<CTCANMsgInfo> ctMsgInfo)
        {
            if (ctMsgInfo == null)
                return;
            Parallel.ForEach(ctMsgInfo, async (item) =>
            {
                try
                {
                    var jsonstring = JsonHelper.ToJson(item);
                    if (!string.IsNullOrEmpty(jsonstring))
                    {
                        var feedback = await CTHttpHelper.CTPostContent(jsonstring, @"/SendMsg");
                        if (!string.IsNullOrEmpty(feedback))
                        {
                            var fb = JsonHelper.JsonDeserialize<CTFeedbackResult>(feedback);
                            if (fb != null && fb.CTResult != CTResultType.OK)
                                Trace.WriteLine(string.Format("{0}: {1}", fb.CTResult, jsonstring));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.StackTrace);
                }
            }
            );
        }
       */
    }
}
