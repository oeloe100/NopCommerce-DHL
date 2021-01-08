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
