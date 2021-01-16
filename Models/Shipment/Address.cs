using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Shipping.DHL.Models.Shipment
{
    public class Address
    {
        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }
        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("street")]
        public string Street { get; set; }
        [JsonProperty("additionalAddressLine")]
        public string AdditionalAddressLine { get; set; }
        [JsonProperty("number")]
        public string Number { get; set; }
        [JsonProperty("isBusiness")]
        public bool IsBusiness { get; set; }
        [JsonProperty("addition")]
        public string Addition { get; set; }
    }
}
