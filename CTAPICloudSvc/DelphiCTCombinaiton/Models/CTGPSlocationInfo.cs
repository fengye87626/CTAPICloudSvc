using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelphiCTCombinaiton.Models
{
    public class CTGPSlocationInfo : CTMsgInfo
    {
        //public string deviceId { get; set; }
        //public string authToken { get; set; }
        public string parameterType { get { return  CTParameterType.GPS.ToString(); } }
        public int  posMethod { get { return 1; } }
        public string posTime { get; set; }
        public int posPrecision { get { return 5; } }
        public string posLongitude { get; set; }
        public string posLatitude { get; set; }
        public int posAltitude { get; set; }
        public int alertStatus { get; set; }
    }
}
