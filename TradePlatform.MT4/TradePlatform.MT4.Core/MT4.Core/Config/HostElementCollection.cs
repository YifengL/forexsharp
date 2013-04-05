using System.Configuration;

namespace TradePlatform.MT4.Core.Config
{
    public class HostElementCollection : ConfigurationElementCollection
    {
        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        protected override string ElementName
        {
            get { return "Host"; }
        }

        public HostElement this[int index]
        {
            get { return (HostElement) base.BaseGet(index); }
            set
            {
                if (base.BaseGet(index) != null)
                {
                    base.BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        public HostElement this[string name]
        {
            get { return (HostElement) base.BaseGet(name); }
        }

        public bool ContainsKey(string name)
        {
            bool flag = false;
            object[] objArray = base.BaseGetAllKeys();
            object[] objArray1 = objArray;
            int num = 0;
            while (num < objArray1.Length)
            {
                object obj = objArray1[num];
                if ((string) obj != name)
                {
                    num++;
                }
                else
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new HostElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((HostElement) element).Name;
        }
    }
}