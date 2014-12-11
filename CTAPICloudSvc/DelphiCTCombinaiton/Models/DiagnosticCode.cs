using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DelphiCTCombinaiton.Models
{
    public class DiagnosticCode
    {
        public DateTimeOffset LastReadDate { get; set; }
        public bool IsCleared { get; set; }
        public string Code { get; set; }
        public Guid Id { get; set; }
        public DTCLink[] _links { get; set; }
    }
    public class DTCLink
    {
        public Link self { get; set; }
        [JsonProperty(PropertyName = "vehicle-diagnostic-codes-list")]
        public Link vehicle_diagnostic_codes_list { get; set; }
    }

    public class DiagnosticCodePageLink
    {
        public Link vehicle { get; set; }
        public Link self { get; set; }
        public Link next { get; set; }
        [JsonProperty(PropertyName = "create-diagnostic")]
        public Link create_diagnostic { get; set; }
    }

    public class DiagnosticCodeJson
    {
        public int Page { get; set; }
        public object _links { get; set; }
        public object _embedded { get; set; }
        public DiagnosticResult[] ResourceList { get; set; }
        
    }

    public class DiagnosticCode_Embeded
    {
        [JsonProperty(PropertyName = "diagnostic-code")]
        public DiagnosticCode[] diagnostic_code { get; set; }
    }
}
