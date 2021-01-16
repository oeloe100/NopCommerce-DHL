using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Shipping.DHL.Models.Shipment
{
    public class OptionsList
    {
        [JsonProperty("key")]
        public string Key { get; set; }
        [JsonProperty("input")]
        public string Input { get; set; }
    }
}
