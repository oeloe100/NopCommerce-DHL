using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Shipping.DHL.Models
{
    public class DHLShippingModel : BaseNopModel
    {
        [NopResourceDisplayName("UseSandbox")]
        public bool UseSandbox { get; set; }
        [NopResourceDisplayName("UserId")]
        public string UserId { get; set; }
        [NopResourceDisplayName("Key")]
        public string Key { get; set; }
        [NopResourceDisplayName("CustomerId")]
        public string AccountNumber { get; set; }
        [NopResourceDisplayName("LocationFinderKey")]
        public string LocationFinderKey { get; set; }
        [NopResourceDisplayName("ApiUrl")]
        public string ApiUrl { get; set; }
    }
}
