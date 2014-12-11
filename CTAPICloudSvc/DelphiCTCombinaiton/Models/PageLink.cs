using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelphiCTCombinaiton.Models
{
    public class Link
    {
        public string href { get; set; }
    }

    public class PageLink
    {
        public Link self { get; set; } //href=/api/v1/geo-fences?page=1
        public Link next { get; set; }//href=/api/v1/geo-fences?page=2
    }
}
