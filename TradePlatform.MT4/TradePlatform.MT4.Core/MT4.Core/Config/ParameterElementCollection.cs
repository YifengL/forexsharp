using System.Configuration;
using System.Linq;

namespace TradePlatform.MT4.Core.Config
{
    public class ParameterElementCollection : ConfigurationElementCollection
    {
        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        protected override string ElementName
        {
            get { return "Parameter"; }
        }

        public ParameterElement this[int index]
        {
            get { return (ParameterElement) base.BaseGet(index); }
            set
            {
                if (base.BaseGet(index) != null)
                {
                    base.BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        public ParameterElement this[string propertyName]
        {
            get { return (ParameterElement) base.BaseGet(propertyName); }
        }

        public bool ContainsKey(string propertyName)
        {
            object[] objArray = base.BaseGetAllKeys();
            return objArray.Any((object obj) => (string) obj == propertyName);
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ParameterElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ParameterElement) element).PropertyName;
        }
    }
}