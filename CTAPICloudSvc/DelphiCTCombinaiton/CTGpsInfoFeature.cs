using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DelphiCTCombinaiton.Models;
using System.Diagnostics;
namespace DelphiCTCombinaiton
{
    public class CTGpsInfoFeature
    {
        public static void PostCTVehicleGPSInfoList(Dictionary<Device, Vehicle> devicevehicle)
        {
            List<CTGPSlocationInfo> GpsInfoLst = new List<CTGPSlocationInfo>();
            var sync = new object();
            try
            {
                Parallel.ForEach(devicevehicle, item =>
                    {lock(sync)
                    {
                        GpsInfoLst.Add(new CTGPSlocationInfo() { deviceId = item.Key.CarrierId, authToken = CTTokenSeeker.CurrentToken.authToken, posAltitude = (int)item.Value.VehicleLocation.Heading, posLatitude = item.Value.VehicleLocation.Location.Latitude.ToString("0.0000"), posLongitude = item.Value.VehicleLocation.Location.Longitude.ToString("0.0000"), posTime = item.Value.UpdatedOn.DateTime.ToLocalTime().ToString("yyyy-MM-dd hh:mm:ss") });
                    }
                    }
                        );
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.StackTrace);
            }
            //PostCTInfo(GpsInfoLst);

            GpsInfoLst.PostContent2CT<CTGPSlocationInfo>(t =>
                {
                    if (t == null)
                        return;
                    CTHttpHelper.PostContent<CTGPSlocationInfo>(t).GetAwaiter();
                });

        }

        /*
       public static void PostCTInfo(List<CTGPSlocationInfo> ctMsgInfo)
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
                                if (fb!=null&& fb.CTResult != CTResultType.OK)
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
