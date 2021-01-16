using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Shipping.DHL.Models.Shipment
{
    public class OptionsParent
    {
        [JsonProperty("options")]
        public List<OptionsList> Options { get; set; }
    }
}
