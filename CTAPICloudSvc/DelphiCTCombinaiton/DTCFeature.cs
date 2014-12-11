using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DelphiCTCombinaiton.Models;

namespace DelphiCTCombinaiton
{
    public class DTCFeature
    {
        public const string VEHICLE_DTC_SCAN = "VEHICLE_DTC_SCAN";
        public const string VEHICLE_DTC_CLEAR = "VEHICLE_DTC_CLEAR";
        public const string SERVER_DTC_CLEAR = "SERVER_DTC_CLEAR";
        const string diagnosticsAddress = "/api/v1/vehicles/{0}/diagnostics";

        const string GetDiagnosticCode = @"/api/v1/diagnostic-codes/{0}";

        const string GetVehicleDTC = @"/api/v1/vehicles/{0}/diagnostic-codes";

        const string GetDTC = @"/api/v1/diagnostics/{0}";

        /*
         *Trigger a DTC scan	POST        /api/v1/vehicles/{ID}/diagnostics
         *Trigger a DTC clear (on Device)	POST /api/v1/vehicles/{ID}/diagnostics
         *Trigger a DTC clear (on Server)	POST /api/v1/vehicles/{ID}/diagnostics
         *Get the state of a Diagnostic	GET      /api/v1/vehicles/{ID}/diagnostics
         */

        public async Task<string> DTCDiagnostics(Guid vehicleID, RequestMethod httpMethod, string postData)
        {
            var hi = new HttpItem() { RelatedAddress = string.Format(diagnosticsAddress, vehicleID), RequestMethod = httpMethod, HttpMsgBodyContent = postData, Authorization = TokenSeeker.CurrentToken.token_type + " " + TokenSeeker.CurrentToken.access_token, Content_Type = "application/json" };

            var t = await (new HTTPHelper(hi)).HttpHelperMethod();

            //Trace.WriteLine(t);
            return t;
        }

        public async Task<string> GetDTCbyID(string DTCId)
        {
            var hi = new HttpItem() { RelatedAddress = string.Format(GetDTC, DTCId), RequestMethod = RequestMethod.Get, HttpMsgBodyContent = null, Authorization = TokenSeeker.CurrentToken.token_type + " " + TokenSeeker.CurrentToken.access_token, Content_Type = "application/json" };

            var t = await (new HTTPHelper(hi)).HttpHelperMethod();

            //Trace.WriteLine(t);
            return t;


        }

        public async Task<string> GetDTCbyVehicleID(Guid vehicleID)
        {
            var hi = new HttpItem() { RelatedAddress = string.Format(GetVehicleDTC, vehicleID), RequestMethod = RequestMethod.Get, HttpMsgBodyContent = null, Authorization = TokenSeeker.CurrentToken.token_type + " " + TokenSeeker.CurrentToken.access_token, Content_Type = "application/json" };

            var t = await (new HTTPHelper(hi)).HttpHelperMethod();

            //Trace.WriteLine(t);
            return t;


        }
    }
}
