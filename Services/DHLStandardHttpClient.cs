using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nop.Core;
using Nop.Plugin.Shipping.DHL.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Shipping.DHL.Services
{
    public class DHLStandardHttpClient
    {
        #region Fields

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient _httpClient;
        private readonly DHLSettings _dhlSettings;

        #endregion

        #region Ctor

        public DHLStandardHttpClient(
            IHttpContextAccessor httpContextAccessor,
            HttpClient httpClient,
            DHLSettings dhlSettings)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpClient = httpClient;
            _dhlSettings = dhlSettings;

            httpClient.BaseAddress = new Uri(dhlSettings.ApiUrl);
            httpClient.Timeout = TimeSpan.FromSeconds(20);
            httpClient.DefaultRequestHeaders.Add(HeaderNames.UserAgent, $"nopCommerce-{NopVersion.CurrentVersion}");

            if (string.IsNullOrEmpty(httpContextAccessor.HttpContext.Request.Cookies["accessToken"]) &&
                string.IsNullOrEmpty(httpContextAccessor.HttpContext.Request.Cookies["refreshToken"]))
            {
                DHLAuthenticateAsync().GetAwaiter().GetResult();
            }
        }

        #endregion

        #region Methods

        private async Task DHLAuthenticateAsync()
        {
            DHLApiRequestModel model = new DHLApiRequestModel()
            {
                UserId = _dhlSettings.UserID,
                Key = _dhlSettings.Key,
                AccountNumber = new List<string>()
                {
                    _dhlSettings.AccountNumber
                }
            };

            string requestMessage = JsonConvert.SerializeObject(model);
            HttpContent content = new StringContent(requestMessage, Encoding.UTF8, "application/json");
            HttpRequestMessage httpReqMessage = new HttpRequestMessage(HttpMethod.Post, "authenticate/api-key")
            {
                Method = HttpMethod.Post,
                Content = content
            };

            try
            {
                HttpResponseMessage httpResponseMessage = await _httpClient.SendAsync(httpReqMessage);
                if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                { 
                    string result = await httpResponseMessage.Content.ReadAsStringAsync();
                    JObject resultObj = JObject.Parse(result);

                    foreach (var item in resultObj)
                    {
                        if (item.Key != "accountNumbers")
                            StoreDataInCookie(item.Key, item.Value.ToObject<string>(), null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new NopException(ex.Message + ex.StackTrace);
            }
        }

        private void StoreDataInCookie(string key, string value, int? expireTime)
        {
            CookieOptions cookieOptions = new CookieOptions();

            if (expireTime.HasValue)
            {
                cookieOptions.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            }

            cookieOptions.HttpOnly = true;

            _httpContextAccessor.HttpContext.Response.Cookies.Append(key, value, cookieOptions);
        }

        #endregion
    }
}
