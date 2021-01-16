using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Nop.Core.Domain.Orders;
using Nop.Services.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Shipping.DHL.Services
{
    public class EventHandler : IConsumer<OrderPaidEvent>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMemoryCache _cache;

        public EventHandler(
            IHttpContextAccessor httpContextAccessor,
            IMemoryCache cache)
        {
            _httpContextAccessor = httpContextAccessor;
            _cache = cache;
        }

        public void HandleEvent(OrderPaidEvent eventMessage)
        {
            List<string> deliveryCacheEntries = new List<string>();

            List<string> entries = new List<string>()
            {
                "deliveryDate",
                "startTime",
                "endTime"
            };

            foreach (var entry in entries)
            {
                deliveryCacheEntries.Add(_cache.Get<string>(entry));
            }

            Console.WriteLine();

            throw new NotImplementedException();
        }
    }
}
