using Nop.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Shipping.DHL
{
    public class DHLSettings : ISettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether to use sandbox environment
        /// </summary>
        public bool UseSandbox { get; set; }

        /// <summary>
        /// DHL UserId
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// Public DHL API Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// DHL User AcccountNumber
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// DHL Developer API Key. For DHL location services.
        /// </summary>
        public string LocationFinderKey { get; set; }

        /// <summary>
        /// dhl api url. default = api-gw.dhlparcel.nl/
        /// </summary>
        public string ApiUrl { get; set; }
    }
}
