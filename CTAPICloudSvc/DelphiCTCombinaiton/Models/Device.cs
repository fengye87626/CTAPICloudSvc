
using System;
namespace DelphiCTCombinaiton.Models
{
    public class Device //api/v1/devices/55e3beba-335b-4716-8a42-e7c5a09a5526
    {
        public DeviceLink _links { get; set; }
        public string ByteId { get; set; }
        public object Capabilities { get; set; }
        public string CarrierId { get; set; }
        public DeviceType DeviceType { get; set; }
        public Guid Id { get; set; }
        public string LabelId { get; set; }
        public string LabelIdType { get; set; }
        public DateTimeOffset LastConnection { get; set; }
        public DeviceProperty[] Properties { get; set; }
        public string RegistrationNumber { get; set; }
    }

    public class DeviceJson //api/v1/customers/883619e1-0cba-4bf1-aa07-5426d633c05c/devices
    {
        public int Page { get; set;}
        public DeviceEmbedded _embedded { get; set; }
        public DeviceLink _links { get; set; }
    }

    public class DeviceLink
    {
        public Link self{get;set;}
        public Link vehicle { get; set; }
        public Link customer { get; set; }
    }

    public class DeviceEmbedded 
    {
        public Device[] device {get;set;}
    }

    public class DeviceType
    {
        public string Created { get; set; }
        public string Description { get; set; }
        public int InternalId { get; set; }
        public string ModelName { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }
    }

    public class DeviceProperty
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class DeviceCapability
    {
        public string Name {get;set;}
        public string Description {get;set;}
        public DateTimeOffset Created { get; set; }
    }
}
