using Microsoft.AspNetCore.Http;
using Nop.Core;
using Nop.Core.Domain.Shipping;
using Nop.Plugin.Shipping.DHL.Services;
using Nop.Services.Configuration;
using Nop.Services.Plugins;
using Nop.Services.Shipping;
using Nop.Services.Shipping.Tracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nop.Plugin.Shipping.DHL
{
    public class DHLCreateShipment : BasePlugin, IShippingRateComputationMethod
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

            //string accessTokenCookieValue = _httpContextAccessor.HttpContext.Request.Cookies["accessToken"];
            //string refreshTokenCookieValue = _httpContextAccessor.HttpContext.Request.Cookies["refreshToken"];

            var options = _dhlService.GetShippingOptionResponse(getShippingOptionRequest);

            return new GetShippingOptionResponse();
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

        #endregion

        #region Utilities

        public ShippingRateComputationMethodType ShippingRateComputationMethodType => ShippingRateComputationMethodType.Realtime;

        public IShipmentTracker ShipmentTracker => new DHLShipmentTracker(_dhlService);

        #endregion
    }
}
