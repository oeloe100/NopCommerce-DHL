using Nop.Core;
using Nop.Services.Configuration;
using Nop.Services.Plugins;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Shipping.DHL
{
    public class DHLCreateShipment : BasePlugin
    {
        #region Fields

        private readonly ISettingService _settingService;
        private readonly IWebHelper _webHelper;

        #endregion

        #region Ctor

        public DHLCreateShipment(
            IWebHelper webHelper,
            ISettingService settingService)
        {
            _webHelper = webHelper;
            _settingService = settingService;
        }

        #endregion

        #region Methods

        public override void Install()
        {
            //settings
            _settingService.SaveSetting(new DHLSettings
            {
                UseSandbox = true,
                UserID = "23KL3LK-2LK4K24-KL2K32..",
                Key = "...",
                Secret = "...",
                AccountNumbers = "213,3232,122.."
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
    }
}
