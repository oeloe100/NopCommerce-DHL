using Microsoft.AspNetCore.Http;
<<<<<<< HEAD
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Nop.Core;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Shipping;
using Nop.Plugin.Shipping.DHL.Models;
using Nop.Services.Directory;
using Nop.Services.Localization;
using Nop.Services.Orders;
=======
using Nop.Core;
using Nop.Core.Domain.Shipping;
>>>>>>> 67aab8c77e0c8f99bb3f15d7ceb45bc156150e5f
using Nop.Services.Shipping;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Nop.Plugin.Shipping.DHL.Services
{
    public class DHLService
    {
<<<<<<< HEAD
=======
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

>>>>>>> 67aab8c77e0c8f99bb3f15d7ceb45bc156150e5f
        #region Methods

        public GetShippingOptionResponse GetShippingOptionResponse(GetShippingOptionRequest getShippingOptionRequest)
        {
            IList<ShippingOption> shippingOptions = new List<ShippingOption>
            {
                new ShippingOption()
                {
<<<<<<< HEAD
                    ShippingRateComputationMethodSystemName = "DHL_Delivery",
                    IsPickupInStore = false,
                    Description = "Uw pakket wordt aan de deur afgeleverd.",
                    Name = "Thuisbezorgd",
                    TransitDays = 1,
                    Rate = 6.5m
                },
                new ShippingOption()
                {
                    ShippingRateComputationMethodSystemName = "DHL_Pickup",
                    IsPickupInStore = true,
                    Description = "Uw pakket wordt afgeleverd bij een door u gekozen DHL afhaalpunt.",
                    Name = "DHL Afhaalpunt",
                    TransitDays = 1,
                    Rate = 6
=======
                    ShippingRateComputationMethodSystemName = "DHL",
                    IsPickupInStore = false,
                    Description = "Uw pakket wordt aan de deur afgeleverd.",
                    Name = "Pakketdienst",
                    TransitDays = 1,
                    Rate = Convert.ToDecimal("6,25")
>>>>>>> 67aab8c77e0c8f99bb3f15d7ceb45bc156150e5f
                }
            };

            GetShippingOptionResponse shippingOptionsResponse = new GetShippingOptionResponse()
            {
                ShippingFromMultipleLocations = false,
                ShippingOptions = shippingOptions,
            };

            return shippingOptionsResponse;
        }

<<<<<<< HEAD
        public JObject BuildShipmentBody()
        {
            ShipmentBodyModel shipmentBodyModel = new ShipmentBodyModel()
            {
                ShipmentId = Guid.NewGuid().ToString(),
            };

            JObject jObject = JObject.FromObject(shipmentBodyModel);

            return jObject;
        } 

=======
>>>>>>> 67aab8c77e0c8f99bb3f15d7ceb45bc156150e5f
        #endregion
    }
}
