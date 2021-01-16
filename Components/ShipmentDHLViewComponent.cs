using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Shipping.DHL.Models;
using Nop.Plugin.Shipping.DHL.Services;
using Nop.Web.Framework.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Shipping.DHL.Components
{
    [ViewComponent(Name = "DHLShipmentWidget")]
    public class ShipmentDHLViewComponent : NopViewComponent
    {
        private readonly DHLStandardHttpClient _dhlStandardHttpClient;

        public ShipmentDHLViewComponent(DHLStandardHttpClient dhlStandardHttpClient)
        {
            _dhlStandardHttpClient = dhlStandardHttpClient;
        }

        public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
        {
            List<DeliveryTableModel> deliverOptionsModel = await _dhlStandardHttpClient.TimeWindowData();
            return View("~/Plugins/Shipping.DHL/Views/PublicInfo.cshtml", deliverOptionsModel);
        }
    }
}
