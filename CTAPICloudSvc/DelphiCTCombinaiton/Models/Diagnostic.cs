using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelphiCTCombinaiton.Models
{
    public class Diagnostic
    {
        public string DiagnosticType { get; set; }
    }

    public class DiagnosticResult
    {
        public object _embedded { get; set; }
        public DiagnosticLink[] _links { get; set; }
        public string Id { get; set; }
        public string Code { get; set; }
        public bool IsCleared { get; set; }
        public DateTimeOffset LastReadDate { get; set; }

        //public DateTimeOffset Created { get; set; }
        //public string DiagnosticType { get; set; }
        //public string Message { get; set; }
        //public DiagnosticState State { get; set; }
    }

    public enum DiagnosticState
    {
        Expired,
        Fail,
        Success,
        InProgress,
        Error
    }

    public class DiagnosticLink
    {
        public string Href { get; set; }
        public bool IsTemplated { get; set; }
        public string Rel { get; set; }
        public string Title { get; set; }
    }

    public class DiagnosticResultJson
    {
        public object _embedded { get; set; }
        public DiagnosticLink[] _links { get; set; }
        public List<DiagnosticResult> ResourceList { get; set; }
    }
}
