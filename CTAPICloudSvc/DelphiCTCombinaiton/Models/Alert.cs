using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DelphiCTCombinaiton.Models
{
    public class Alert //href=/api/v1/alerts/P1134R2520197879779469999
    {
        public AlertLinks _links{get;set;}
        public string AlertType {get;set;}
        public Guid Id{get;set;}
        public bool IsDisplayed {get;set;}
        public DateTimeOffset TriggeredDateTime{get;set;}
        public object Value{get;set;}
    }

    public class AlertLinks
    {
        public Link self { get; set; }
        public Link vehicle { get; set; }
        [JsonProperty(PropertyName="vehicle-alert-list")]
        public Link vehicle_alert_list{get;set;}
    }


    public class AlertsJson  ///api/v1/vehicles/b3f9b64c-2059-47ef-8438-56261f67fdcb/alerts
    {
        public int Page { get; set; }
        public PageLink _links { get; set; }
        public AlertEmbed _embedded { get; set; }
    }

    //public class AlertPageLink
    //{
    //    public Link self { get; set; } //href=/api/v1/vehicles/b3f9b64c-2059-47ef-8438-56261f67fdcb/alerts?page=1
    //}

    public class AlertEmbed
    {
        public Alert[] alert { get; set; }
    }
}
