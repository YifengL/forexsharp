using System;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace TradePlatform.MT4.Core.Config
{
	public class ParameterElementCollection : ConfigurationElementCollection
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
				return "Parameter";
			}
		}

		public ParameterElement this[int index]
		{
			get
			{
				return (ParameterElement)base.BaseGet(index);
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

		public ParameterElement this[string propertyName]
		{
			get
			{
				return (ParameterElement)base.BaseGet(propertyName);
			}
		}

		public ParameterElementCollection()
		{
		}

		public bool ContainsKey(string propertyName)
		{
			object[] objArray = base.BaseGetAllKeys();
			return objArray.Any<object>((object obj) => (string)obj == propertyName);
		}

		protected override ConfigurationElement CreateNewElement()
		{
			return new ParameterElement();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ParameterElement)element).PropertyName;
		}
	}
}