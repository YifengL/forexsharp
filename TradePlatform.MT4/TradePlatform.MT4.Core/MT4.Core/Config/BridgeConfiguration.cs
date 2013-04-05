using System.Configuration;

namespace TradePlatform.MT4.Core.Config
{
    public class BridgeConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("Hosts")]
        public HostElementCollection Hosts
        {
            get { return base["Hosts"] as HostElementCollection; }
        }

        [ConfigurationProperty("useEventLog", IsRequired = true, IsKey = true)]
        public bool UseEventLog
        {
            get { return (bool) base["useEventLog"]; }
            set { base["useEventLog"] = value; }
        }

        [ConfigurationProperty("wcfBaseAddress", IsRequired = true, IsKey = true)]
        public string WcfBaseAddress
        {
            get { return (string) base["wcfBaseAddress"]; }
            set { base["wcfBaseAddress"] = value; }
        }
    }
}