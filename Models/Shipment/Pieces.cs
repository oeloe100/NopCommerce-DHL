using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Shipping.DHL.Models.Shipment
{
    public class Pieces
    {
        [JsonProperty("parcelType")]
        public string ParcelType { get; set; }
        [JsonProperty("quantity")]
        public string Quantity { get; set; }
        [JsonProperty("weight")]
        public string Weight { get; set; }
        [JsonProperty("dimensions")]
        public Dimensions Dimensions { get; set; }
    }
}
