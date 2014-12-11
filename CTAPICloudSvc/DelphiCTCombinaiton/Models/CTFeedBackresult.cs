using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelphiCTCombinaiton.Models
{
    public class CTFeedbackResult
    {
        public CTResultType CTResult { get; set; }
        public string Note { get; set; }
    }

    public enum CTResultType : int
    {
        OK = 0,
        MessageFormatError = 1,
        ParameterTypeUnknown = 2,
        authTokenIllegal = 100,
        authTokenInvalid = 101
    }
}
