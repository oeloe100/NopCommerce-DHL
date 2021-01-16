using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Nop.Plugin.Shipping.DHL.Domain;
using Nop.Plugin.Shipping.DHL.Models;

namespace Nop.Plugin.Shipping.DHL.Controllers
{
    public class DeliveryDataController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMemoryCache _cache;

        public DeliveryDataController(
            IHttpContextAccessor httpContextAccessor,
            IMemoryCache cache)
        {
            _httpContextAccessor = httpContextAccessor;
            _cache = cache;
        }

        #region Methods

        [HttpPost]
        public IActionResult SaveData([FromForm] DeliveryTableModel model)
        {
            TimeSpan expireTime = TimeSpan.FromDays(2);
            StoreDataInMemory("deliveryDate", model.DeliveryDate, expireTime);
            StoreDataInMemory("startTime", model.StartTime, expireTime);
            StoreDataInMemory("endTime", model.EndTime, expireTime);

            return Ok();
        }

        private void StoreDataInMemory(string key, string value, TimeSpan expireTime)
        {
            if (!_cache.TryGetValue(key, out string cacheEntry))
            {
                cacheEntry = value;

                var cacheEntryOptions = new MemoryCacheEntryOptions().
                    SetSlidingExpiration(expireTime);

                _cache.Set(key, cacheEntry, cacheEntryOptions);
            }
        }

        #endregion
    }
}
