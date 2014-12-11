using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DelphiCTCombinaiton.Models;
using System.Diagnostics;

namespace DelphiCTCombinaiton
{
    public class CTHttpHelper
    {
        public static async Task<string> CTPostContent(string postcontent,string address)
        {
            //Trace.WriteLine(postcontent);
            //HttpItem hi = new HttpItem() { AddressBase = @"http://dev.cpsdna.org:22299", RelatedAddress = address, RequestMethod = RequestMethod.Post, HttpMsgBodyContent = postcontent, Content_Type = "application/json" };
            HttpItem hi = new HttpItem() { AddressBase = @"http://101.95.48.125:9090", RelatedAddress = address, RequestMethod = RequestMethod.Post, HttpMsgBodyContent = postcontent, Content_Type = "application/json" };
            var result =await new HTTPHelper(hi).HttpHelperMethod();
            return result;
        }

        public static async Task PostContent<T>(T t)
        {
            try
            {
                var jsonstring = JsonHelper.ToJson(t);
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

    }
}
