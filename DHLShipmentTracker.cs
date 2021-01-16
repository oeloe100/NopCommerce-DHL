using Nop.Plugin.Shipping.DHL.Services;
using Nop.Services.Shipping.Tracking;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Shipping.DHL
{
    public class DHLShipmentTracker : IShipmentTracker
    {
        private readonly DHLService _dhlService;

        public DHLShipmentTracker(DHLService dhlService)
        {
            _dhlService = dhlService;
        }

        public IList<ShipmentStatusEvent> GetShipmentEvents(string trackingNumber)
        {
            throw new NotImplementedException();
        }

        public string GetUrl(string trackingNumber)
        {
            throw new NotImplementedException();
        }

        public bool IsMatch(string trackingNumber)
        {
            throw new NotImplementedException();
        }
    }
}
