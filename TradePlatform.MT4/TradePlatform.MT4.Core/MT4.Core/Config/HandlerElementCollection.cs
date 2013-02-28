using System;
using System.Configuration;
using System.Reflection;

namespace TradePlatform.MT4.Core.Config
{
	public class HandlerElementCollection : ConfigurationElementCollection
	{
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		protected override string ElementName
		{
			get
			{
				return "Handler";
			}
		}

		public HandlerElement this[int index]
		{
			get
			{
				return (HandlerElement)base.BaseGet(index);
			}
			set
			{
				if (base.BaseGet(index) != null)
				{
					base.BaseRemoveAt(index);
				}
				this.BaseAdd(index, value);
			}
		}

		public HandlerElement this[string name]
		{
			get
			{
				return (HandlerElement)base.BaseGet(name);
			}
		}

		public HandlerElementCollection()
		{
		}

		public bool ContainsKey(string name)
		{
			bool flag = false;
			object[] objArray = base.BaseGetAllKeys();
			object[] objArray1 = objArray;
			int num = 0;
			while (num < (int)objArray1.Length)
			{
				object obj = objArray1[num];
				if ((string)obj != name)
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
			return new HandlerElement();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((HandlerElement)element).Name;
		}
	}
}