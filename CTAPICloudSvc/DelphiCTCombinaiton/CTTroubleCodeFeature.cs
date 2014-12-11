using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DelphiCTCombinaiton.Models;
using System.Diagnostics;

namespace DelphiCTCombinaiton
{
    public class CTTroubleCodeFeature
    {
        static DTCFeature dtctest = new DTCFeature();

        public void PostCTTroubleCodeInfoList(Dictionary<Device, Vehicle> devicevehicle)
        {
            List<CTTroubleCodeInfo> TcodesInfoLst = new List<CTTroubleCodeInfo>();
            var sync = new object();
            try
            {
                Parallel.ForEach(devicevehicle, item =>
                    {
                        var dtcstring = dtctest.GetDTCbyVehicleID(item.Value.Id).Result;
                        var dtcs = JsonHelper.JsonDeserialize<DiagnosticResultJson>(dtcstring);
                        if (dtcs != null & dtcs.ResourceList != null)
                        {
                            try
                            {
                                lock (sync)
                                {
                                    foreach (var dtccode in dtcs.ResourceList.ToLookup(c => c.LastReadDate))
                                    {
                                        List<CTTroubleCode> dtccodeArry = new List<CTTroubleCode>();
                                        //Trace.WriteLine(dtccode.ToList());
                                        foreach (var itemad in dtccode.ToList())
                                        {
                                            dtccodeArry.Add(new CTTroubleCode() { troubleCode = itemad.Code });
                                        }
                                        TcodesInfoLst.Add(new CTTroubleCodeInfo() { authToken = CTTokenSeeker.CurrentToken.authToken, deviceId = item.Key.CarrierId, reportTime = dtccode.Key.ToLocalTime().ToString("yyyy-MM-dd hh:mm:ss"), troubleCodeAll = dtccodeArry.ToArray() });
                                        //Trace.WriteLine(dtccode);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Trace.WriteLine(ex.StackTrace);
                            }

                        }
                    }
                    );
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.StackTrace);
            }
            //PostCTInfo(TcodesInfoLst);

            TcodesInfoLst.PostContent2CT<CTTroubleCodeInfo>(t =>
            {
                if (t == null)
                    return;
                CTHttpHelper.PostContent<CTTroubleCodeInfo>(t).GetAwaiter();
            });
        }

        #region
        /*
        public async Task<CTFeedbackResult> PostDTC(string postcontent)
        {
            var result = await CTHttpHelper.CTPostContent(postcontent, @"/SendMsg");
            var feedback = JsonHelper.JsonDeserialize<CTFeedbackResult>(result);
            return feedback;
        }

        public void PostCTInfo(List<CTTroubleCodeInfo> ctMsgInfo)
        {
            if (ctMsgInfo == null)
                return;
            string feedback = string.Empty;
            //foreach (var item in ctMsgInfo)
            //{
            //    var jsonstring = JsonHelper.ToJson(item);
            //    if (jsonstring != null)
            //        feedback = CTHttpHelper.CTPostContent(jsonstring, @"/SendMsg");
            //    var fb = JsonHelper.JsonDeserialize<CTFeedbackResult>(feedback);
            //    //DODO:
            //    if (fb.CTResult != CTResultType.OK)
            //        Trace.WriteLine(string.Format("{0}: {1}", fb.CTResult, jsonstring));
            //}
            try
            {
                Parallel.ForEach(ctMsgInfo, async (item) =>
                {
                    var jsonstring = JsonHelper.ToJson(item);
                    if (jsonstring != null)
                        feedback = await CTHttpHelper.CTPostContent(jsonstring, @"/SendMsg");
                    var fb = JsonHelper.JsonDeserialize<CTFeedbackResult>(feedback);
                    //DODO:
                    if (fb != null && fb.CTResult != CTResultType.OK)
                        Trace.WriteLine(string.Format("{0}: {1}", fb.CTResult, jsonstring));
                }
                    );
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.StackTrace);
            }
        }

        */
        #endregion
    }

}

