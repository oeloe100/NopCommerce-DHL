using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Shipping.DHL.Models
{
    public class DHLApiRequestModel
    {
        [JsonProperty("key")]
        public string Key { get; set; }
        [JsonProperty("userId")]
        public string UserId { get; set; }
        public List<string> AccountNumber { get; set; }
    }
}
