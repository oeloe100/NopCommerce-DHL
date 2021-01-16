using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Shipping.DHL.Models.Shipment
{
    public class OnBehalfOf
    {
        [JsonProperty("name")]
        public Name Name { get; set; }
        [JsonProperty("address")]
        public Address Address { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }
        [JsonProperty("vatNumber")]
        public string VatNumber { get; set; }
        [JsonProperty("eori")]
        public string Eori { get; set; }
    }
}
