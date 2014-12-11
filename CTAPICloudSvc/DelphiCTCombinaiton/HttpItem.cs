using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using DelphiCTCombinaiton.Models;

namespace DelphiCTCombinaiton
{
    public class HttpItem
    {
        public string AddressBase { get; set; }
        public string RelatedAddress { get; set; }
        public RequestMethod RequestMethod { get; set; }
        //public string HostName { get; set; } //Host: hk-uat-sharedapi.delphiconnect.com
        public object HttpMsgBodyContent { get; set; }
        public string Content_Type { get; set; } //Content-Type: application/hal+json
        public string Authorization { get; set; }// Authorization: Bearer boQtj0SCGz2GFG==
    }

    public enum RequestMethod
    {
        Post,
        Get,
        Delete,
        Put
    }

}
