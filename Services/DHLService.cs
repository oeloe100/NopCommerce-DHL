using Microsoft.AspNetCore.Http;
using Nop.Core;
using Nop.Core.Domain.Shipping;
using Nop.Services.Shipping;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Nop.Plugin.Shipping.DHL.Services
{
    public class DHLService
    {
        #region Fields

        private readonly DHLStandardHttpClient _dhlHttpClient;

        #endregion

        #region Ctor

        public DHLService(
            DHLStandardHttpClient dhlHttpClient)
        {
            _dhlHttpClient = dhlHttpClient;
        }

        #endregion

        #region Methods

        public GetShippingOptionResponse GetShippingOptionResponse(GetShippingOptionRequest getShippingOptionRequest)
        {
            IList<ShippingOption> shippingOptions = new List<ShippingOption>
            {
                new ShippingOption()
                {
                    ShippingRateComputationMethodSystemName = "DHL",
                    IsPickupInStore = false,
                    Description = "Uw pakket wordt aan de deur afgeleverd.",
                    Name = "Pakketdienst",
                    TransitDays = 1,
                    Rate = Convert.ToDecimal("6,25")
                }
            };

            GetShippingOptionResponse shippingOptionsResponse = new GetShippingOptionResponse()
            {
                ShippingFromMultipleLocations = false,
                ShippingOptions = shippingOptions,
            };

            return shippingOptionsResponse;
        }

        #endregion
    }
}
