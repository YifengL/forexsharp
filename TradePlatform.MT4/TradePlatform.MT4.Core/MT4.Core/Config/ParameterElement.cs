using System.Configuration;

namespace TradePlatform.MT4.Core.Config
{
    public class ParameterElement : ConfigurationElement
    {
        [ConfigurationProperty("propertyName", IsRequired = true, IsKey = true)]
        public string PropertyName
        {
            get { return base["propertyName"] as string; }
            set { base["propertyName"] = value; }
        }

        [ConfigurationProperty("propertyValue", IsRequired = true, IsKey = false)]
        public string PropertyValue
        {
            get { return base["propertyValue"] as string; }
            set { base["propertyValue"] = value; }
        }
    }
}