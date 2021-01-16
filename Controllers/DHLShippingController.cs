using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Shipping.DHL.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Shipping.DHL.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class DHLShippingController : BasePluginController
    {
        #region Fields

        private readonly IPermissionService _permissionService;
        private readonly INotificationService _notificationService;
        private readonly ILocalizationService _localizationService;
        private readonly ISettingService _settingService;
        private readonly IStoreContext _storeContext;
        private readonly DHLSettings _settings;

        #endregion

        #region Ctor

        public DHLShippingController(
            IPermissionService permissionService,
            INotificationService notificationService,
            ILocalizationService localizationService,
            ISettingService settingSevice,
            IStoreContext storeContext,
            DHLSettings settings)
        {
            _permissionService = permissionService;
            _notificationService = notificationService;
            _localizationService = localizationService;
            _settingService = settingSevice;
            _storeContext = storeContext;
            _settings = settings;
        }

        #endregion

        #region Methods

        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        public IActionResult Configure()
        {
            //whether user has the authority to manage configuration
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageShippingSettings))
                return AccessDeniedView();

            //prepare common model
            var model = new DHLShippingModel()
            {
                UseSandbox = _settings.UseSandbox,
                UserId = _settings.UserID,
                Key = _settings.Key,
                AccountNumber = _settings.AccountNumber,
                LocationFinderKey = _settings.LocationFinderKey,
                ApiUrl = _settings.ApiUrl,
            };

            return View("~/Plugins/Shipping.DHL/Views/Configure.cshtml", model);
        }

        [HttpPost]
        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        public IActionResult Configure(DHLShippingModel model)
        {
            //whether user has the authority to manage configuration
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageShippingSettings))
                return AccessDeniedView();

            if (!ModelState.IsValid)
                return Configure();

            _settings.UseSandbox = model.UseSandbox;
            _settings.AccountNumber = model.AccountNumber;
            _settings.Key = model.Key;
            _settings.UserID = model.UserId;
            _settings.UseSandbox = model.UseSandbox;
            _settings.LocationFinderKey = model.LocationFinderKey;
            _settings.ApiUrl = model.ApiUrl;

            _settingService.SaveSetting(_settings);

            _notificationService.SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));

            return Configure();
        }

        #endregion
    }
}
