using Newtonsoft.Json;
using Nop.Plugin.Shipping.DHL.Models.Shipment;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Shipping.DHL.Models
{
    public class ShipmentBodyModel
    {
        [JsonProperty("shipmentId")]
        public string ShipmentId { get; set; }
        [JsonProperty("orderReference")]
        public string OrderReference { get; set; }
        [JsonProperty("Receiver")]
        public Receiver Receiver { get; set; }
        [JsonProperty("Shipper")]
        public Shipper Shipper { get; set; }
        [JsonProperty("accountId")]
        public string AccountId { get; set; }
        [JsonProperty("options")]
        public OptionsParent Options { get; set; }
        [JsonProperty("onBehalfOf")]
        public OnBehalfOf OnBehalfOf { get; set; }
        [JsonProperty("returnLabel")]
        public string ReturnLabel { get; set; }
        [JsonProperty("pieces")]
        public List<Pieces> Pieces { get; set; }
    }
}
