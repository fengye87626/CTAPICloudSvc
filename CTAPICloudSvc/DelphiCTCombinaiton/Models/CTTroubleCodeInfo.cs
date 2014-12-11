using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelphiCTCombinaiton.Models
{
   public  class CTTroubleCodeInfo
    {
       public string deviceId { get; set; }
       public string authToken { get; set; }
       public string parameterType { get { return CTParameterType.TCODE.ToString(); } }
       public CTTroubleCode[] troubleCodeAll { get; set; }
       public string reportTime { get; set; }
    }


   public class CTTroubleCode
   {
       public string troubleCode { get; set; }
   }
}
