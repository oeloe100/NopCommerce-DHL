using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Shipping.DHL.Models;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Shipping.DHL.Controllers
{
    [AuthorizeAdmin]
    [Area(AreaNames.Admin)]
    [AutoValidateAntiforgeryToken]
    public class DHLShippingController : BasePluginController
    {
        #region Fields

        private readonly IPermissionService _permissionService;
        private readonly DHLSettings _settings;

        #endregion

        #region Ctor

        public DHLShippingController(
            IPermissionService permissionService,
            DHLSettings settings)
        {
            _permissionService = permissionService;
            _settings = settings;
        }

        #endregion

        #region Methods

        public IActionResult Configure()
        {
            //whether user has the authority to manage configuration
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageShippingSettings))
                return AccessDeniedView();

            //prepare common model
            var model = new DHLShippingModel
            {
                UseSandbox = _settings.UseSandbox,
                UserId = _settings.UserID,
                Secret = _settings.Secret,
                Key = _settings.Key,
                AccountNumbers = _settings.AccountNumbers
            };

            return View("~/Plugins/Shipping.DHL/Views/Configure.cshtml", model);
        }

        [HttpPost]
        public IActionResult Configure(DHLShippingModel model)
        {
            //whether user has the authority to manage configuration
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageShippingSettings))
                return AccessDeniedView();

            if (!ModelState.IsValid)
                return Configure();

            _settings.UseSandbox = model.UseSandbox;
            _settings.AccountNumbers = model.AccountNumbers;
            _settings.Key = model.Key;
            _settings.Secret = model.Secret;
            _settings.UserID = model.UserId;
            _settings.UseSandbox = model.UseSandbox;

            return Configure();
        }

        #endregion
    }
}
