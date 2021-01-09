using Nop.Web.Framework.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Shipping.DHL.Models
{
    public class DHLShippingModel
    {
        [NopResourceDisplayName("UseSandbox")]
        public bool UseSandbox { get; set; }
        [NopResourceDisplayName("UserId")]
        public string UserId { get; set; }
        [NopResourceDisplayName("Key")]
        public string Key { get; set; }
        [NopResourceDisplayName("Secret")]
        public string Secret { get; set; }
        [NopResourceDisplayName("AccountNumbers")]
        public string AccountNumbers { get; set; }
    }
}
