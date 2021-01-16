using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Shipping.DHL.Models
{
    public class DeliveryTableModel
    {
        public string PostalCode { get; set; }
        public string DeliveryDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
