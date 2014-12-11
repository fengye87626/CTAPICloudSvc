using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelphiCTCombinaiton.Models
{
    public class CTCANMsgInfo:CTMsgInfo
    {
        //public string deviceId { get; set; }
        //public string authToken { get; set; }
        public string parameterType { get { return CTParameterType.CAN.ToString(); } }
        public string vin { get; set; }
        public int obdStd { get{return 1;}}

        public int vehicleSpeed { get; set; }
        public int engineRpm { get; set; }
        public string posTime { get; set; }
        public string fuelPressure { get; set; }
        public int distanceType { get { return 2; } }
        public int distanceTotal { get; set; }
        public double engineCoolliquidTemp{ get; set; }

        public double  storageBatteryVoltage { get; set; }
        public string posLongitude { get; set; }
        public string posLatitude { get; set; }
        public int posAltitude { get; set; }
    }
}
