using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DelphiCTCombinaiton.Models
{
    public class AlertConfiguration
    {
        public string State { get; set; }
        public string Category { get; set; }
        public Guid Id { get; set; }
        public Notification[] Notifications { get; set; }
        public string AlertType { get; set; }

        public AlertConfigurationLinks _links { get; set; }
        public AlertConfigurationParameter[] Parameters { get; set; }

    }
    public class AlertConfigurationLinks
    {
        public Link self { get; set; }
        public Link vehicle { get; set; }
        [JsonProperty(PropertyName = "vehicle-alert-configuration-list")]
        public Link vehicle_alert_configuration_list { get; set; }
    }

    public class AlertConfigurationParameter
    {
        public string ParameterType { get; set; }
        public string Value { get; set; }
    }

    public class Notification
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }

}
