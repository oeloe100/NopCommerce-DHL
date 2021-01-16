using Microsoft.AspNetCore.Http;
using Nop.Core;
using Nop.Core.Domain.Shipping;
using Nop.Plugin.Shipping.DHL.Domain;
using Nop.Plugin.Shipping.DHL.Services;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Plugins;
using Nop.Services.Shipping;
using Nop.Services.Shipping.Tracking;
using Nop.Web.Framework.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nop.Plugin.Shipping.DHL
{
    public class DHLCreateShipment : BasePlugin, IShippingRateComputationMethod, IWidgetPlugin
    {
        #region Fields

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISettingService _settingService;
        private readonly IWebHelper _webHelper;
        private readonly DHLService _dhlService;
        private readonly DHLStandardHttpClient _dhlHttpClient;

        #endregion

        #region Ctor

        public DHLCreateShipment(
            IHttpContextAccessor httpContextAccessor,
            IWebHelper webHelper,
            ISettingService settingService,
            DHLService dhlService,
            DHLStandardHttpClient dhlHttpClient)
        {
            _httpContextAccessor = httpContextAccessor;
            _webHelper = webHelper;
            _settingService = settingService;
            _dhlService = dhlService;
            _dhlHttpClient = dhlHttpClient;
        }

        #endregion

        #region Methods


        public GetShippingOptionResponse GetShippingOptions(GetShippingOptionRequest getShippingOptionRequest)
        {
            if (getShippingOptionRequest == null)
                throw new ArgumentNullException(nameof(getShippingOptionRequest));

            if (!getShippingOptionRequest.Items?.Any() ?? true)
                return new GetShippingOptionResponse { Errors = new[] { "No shipment items" } };

            if (getShippingOptionRequest.ShippingAddress?.CountryId == null)
                return new GetShippingOptionResponse { Errors = new[] { "Shipping address is not set" } };

            List<CapabilitesOptions.Options> capabilitesOptions = new List<CapabilitesOptions.Options>
            {
                CapabilitesOptions.Options.DOOR,
                CapabilitesOptions.Options.NBB
            };

            var capabilites = _dhlHttpClient.CapabilitesBuilder(
                fromCountry: CountryCode.ISO.NL, toCountry: CountryCode.ISO.NL, 
                parcelType: ParcelType.Parcel.SMALL, capabilitiesOptions: capabilitesOptions, 
                "9721TH", "5705CL", "Helmond").GetAwaiter().GetResult();

            return _dhlService.GetShippingOptionResponse(getShippingOptionRequest);
        }

        public decimal? GetFixedRate(GetShippingOptionRequest getShippingOptionRequest)
        {
            return null;
        }

        public override void Install()
        {
            //settings
            _settingService.SaveSetting(new DHLSettings
            {
                UseSandbox = true,
                UserID = " ",
                Key = " ",
                AccountNumber = " ",
                LocationFinderKey = " ",
                ApiUrl = "https://api-gw.dhlparcel.nl"
            });

            base.Install();
        }

        /// <summary>
        /// Uninstall plugin
        /// </summary>
        public override void Uninstall()
        {
            //settings
            _settingService.DeleteSetting<DHLSettings>();

            base.Uninstall();
        }

        /// <summary>
        /// Gets a configuration page URL
        /// </summary>
        public override string GetConfigurationPageUrl()
        {
            return $"{_webHelper.GetStoreLocation()}Admin/DHLShipping/Configure";
        }

        public IList<string> GetWidgetZones()
        {
            return new List<string> 
            { 
                PublicWidgetZones.OpCheckoutShippingMethodBottom
            };
        }

        public string GetWidgetViewComponentName(string widgetZone)
        {
            if (widgetZone == null)
                throw new ArgumentNullException(nameof(widgetZone));

            return "DHLShipmentWidget";
        }

        #endregion

        #region Utilities

        public ShippingRateComputationMethodType ShippingRateComputationMethodType => ShippingRateComputationMethodType.Realtime;

        public IShipmentTracker ShipmentTracker => new DHLShipmentTracker(_dhlService);

        public bool HideInWidgetList => false;

        #endregion
    }
}
