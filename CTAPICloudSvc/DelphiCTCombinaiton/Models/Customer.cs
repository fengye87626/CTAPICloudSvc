using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DelphiCTCombinaiton.Models
{
    public class Customer
    {
        public string DefaultLanguage { get; set; }
        public string DefaultMeasurementSystem { get; set; }
        public string Email { get; set; }
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LoginStatus { get; set; }
        public string UserName { get; set; }
        public string UserAccessMask { get; set; }
        public Guid TenantId { get; set; }
        public object _embedded { get; set; }
        public CustomerLink _links { get; set; }
    }

    public class CustomerLink
    {
        [JsonProperty(PropertyName = "customer-device-list")]
        public Link customer_device_list { get; set; }
        [JsonProperty(PropertyName = "customer-vehicle-list")]
        public Link customer_vehicle_list { get; set; }
        [JsonProperty(PropertyName = "self")]
        public Link self { get; set; }
    }

    public class CustomerJson
    {
        public int Page { get; set; }
        public PageLink _links { get; set; }
        public CustomerList _embedded { get; set; }
    }

    public class CustomerList
    {
       public List<Customer> customer {get;set;}
    }

    public class CustCred
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

}
