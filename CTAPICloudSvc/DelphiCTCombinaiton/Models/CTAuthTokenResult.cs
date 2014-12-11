using System;

namespace DelphiCTCombinaiton.Models
{
    public  class CTAuthTokenResult
    {
        public CTResultType result { get; set; }
        public string authToken { get; set; }
        public string validTime { get; set; }
    }
}
