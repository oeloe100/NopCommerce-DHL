using Nop.Web.Framework.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Shipping.DHL.Models
{
    public class DHLShippingModel
    {
        [NopResourceDisplayName("Plugins.Shipping.DHL.Fields.UseSandbox")]
        public bool UseSandbox { get; set; }
    }
}
