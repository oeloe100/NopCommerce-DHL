using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nop.Core;
<<<<<<< HEAD
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Infrastructure;
using Nop.Plugin.Shipping.DHL.Domain;
using Nop.Plugin.Shipping.DHL.Models;
using Nop.Services.Customers;
using Nop.Services.Orders;
using Nop.Services.Shipping;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
=======
using Nop.Plugin.Shipping.DHL.Models;
using System;
using System.Collections.Generic;
>>>>>>> 67aab8c77e0c8f99bb3f15d7ceb45bc156150e5f
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Shipping.DHL.Services
{
    public class DHLStandardHttpClient
    {
        #region Fields

        private readonly IHttpContextAccessor _httpContextAccessor;
<<<<<<< HEAD
        private readonly IWorkContext _workContext;
=======
>>>>>>> 67aab8c77e0c8f99bb3f15d7ceb45bc156150e5f
        private readonly HttpClient _httpClient;
        private readonly DHLSettings _dhlSettings;

        #endregion

        #region Ctor

        public DHLStandardHttpClient(
            IHttpContextAccessor httpContextAccessor,
<<<<<<< HEAD
            IWorkContext workContext,
=======
>>>>>>> 67aab8c77e0c8f99bb3f15d7ceb45bc156150e5f
            HttpClient httpClient,
            DHLSettings dhlSettings)
        {
            _httpContextAccessor = httpContextAccessor;
<<<<<<< HEAD
            _workContext = workContext;
=======
>>>>>>> 67aab8c77e0c8f99bb3f15d7ceb45bc156150e5f
            _httpClient = httpClient;
            _dhlSettings = dhlSettings;

            httpClient.BaseAddress = new Uri(dhlSettings.ApiUrl);
            httpClient.Timeout = TimeSpan.FromSeconds(20);
            httpClient.DefaultRequestHeaders.Add(HeaderNames.UserAgent, $"nopCommerce-{NopVersion.CurrentVersion}");

<<<<<<< HEAD
            //DHLAuthenticateAsync().GetAwaiter().GetResult();

=======
>>>>>>> 67aab8c77e0c8f99bb3f15d7ceb45bc156150e5f
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
<<<<<<< HEAD

        #region Utilities

        public async Task<JObject> CapabilitesBuilder(
            [NotNull] CountryCode.ISO fromCountry,
            [NotNull] CountryCode.ISO toCountry,
            [NotNull] ParcelType.Parcel parcelType,
            [NotNull] List<CapabilitesOptions.Options> capabilitiesOptions,
            string fromPostalCode,
            string toPostalCode,
            string toCity,
            bool toBusiness = false,
            bool returnableProduct = false)
        {
            var capabilities = "";
            string dateFormatted = string.Format("{0}-{1}-{2}", DateTime.UtcNow.Year, 
                DateTime.UtcNow.Month.ToString("00"), DateTime.UtcNow.Day.ToString("00"));

            StringBuilder urlBuilder = new StringBuilder();
            urlBuilder.AppendFormat("capabilities/business?fromCountry={0}", fromCountry);
            urlBuilder.AppendFormat("&toCountry={0}", toCountry);
            urlBuilder.AppendFormat("&toBusiness={0}", toBusiness.ToString().ToLower());
            urlBuilder.AppendFormat("&returnProduct={0}", returnableProduct.ToString().ToLower());
            urlBuilder.AppendFormat("&parcelType={0}", parcelType);

            for (var i = 0; i < capabilitiesOptions.Count; i++)
            {
                capabilities += capabilitiesOptions[i] + ",";
            }

            urlBuilder.AppendFormat("&option={0}", capabilities.ToString().TrimEnd(','));
            urlBuilder.AppendFormat("&fromPostalCode={0}", fromPostalCode);
            urlBuilder.AppendFormat("&toPostalCode={0}", toPostalCode);
            urlBuilder.AppendFormat("&toCity={0}", toCity);
            urlBuilder.AppendFormat("&accountNumber={0}", _dhlSettings.AccountNumber);
            urlBuilder.AppendFormat("&organisationId={0}", _dhlSettings.UserID);
            urlBuilder.AppendFormat("&businessUnit={0}", "dhl-nl");
            urlBuilder.AppendFormat("&carrier={0}", "DHL-PARCEL");
            urlBuilder.AppendFormat("&referenceTimeStamp={0}", dateFormatted);

            string url = _dhlSettings.ApiUrl + urlBuilder;

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    JArray resultObj = JArray.Parse(result);

                    foreach (var item in resultObj)
                    {
                        return item as JObject;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                return JObject.Parse(ex.Message);
            }
        }

        public async Task<List<DeliveryTableModel>> TimeWindowData()
        {
            List<DeliveryTableModel> deliveryOptions = new List<DeliveryTableModel>();

            ICustomerService customerService = EngineContext.Current.Resolve<ICustomerService>();
            Customer customer = _workContext.CurrentCustomer;

            Address customerShippingAddress = customerService.GetCustomerShippingAddress(customer);

            StringBuilder urlBuilder = new StringBuilder();
            urlBuilder.AppendFormat("time-windows?countryCode={0}", CountryCode.ISO.NL);
            urlBuilder.AppendFormat("&postalCode={0}", customerShippingAddress.ZipPostalCode);
            urlBuilder.AppendFormat("&rules={0}", Rules.Rule.b2c);
            urlBuilder.AppendFormat("&excludeDaysOfWeek={0}", 7);

            string url = _dhlSettings.ApiUrl + urlBuilder;

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    JArray resultObj = JArray.Parse(result);

                    foreach (var item in resultObj)
                    {
                        DeliveryTableModel model = new DeliveryTableModel()
                        {
                            PostalCode = item["postalCode"].Value<string>(),
                            DeliveryDate = item["deliveryDate"].Value<string>(),
                            StartTime = item["startTime"].Value<string>(),
                            EndTime = item["endTime"].Value<string>()
                        };

                        deliveryOptions.Add(model);
                    }
                }

                return deliveryOptions;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
=======
>>>>>>> 67aab8c77e0c8f99bb3f15d7ceb45bc156150e5f
    }
}
