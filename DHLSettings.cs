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
        /// Private DHL API secret
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// DHL User Acccount Number(s) , seperated
        /// </summary>
        public string AccountNumbers { get; set; }
    }
}
