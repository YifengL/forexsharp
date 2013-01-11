using System;
using System.Configuration;

namespace TradePlatform.MT4.Core.Config
{
	public class HandlerElement : ConfigurationElement
	{
		[ConfigurationProperty("assemblyName", IsRequired=true, IsKey=false)]
		public string AssemblyName
		{
			get
			{
				return base["assemblyName"] as string;
			}
			set
			{
				base["assemblyName"] = value;
			}
		}

		[ConfigurationProperty("Parameters")]
		public ParameterElementCollection InputParameters
		{
			get
			{
				return base["Parameters"] as ParameterElementCollection;
			}
		}

		[ConfigurationProperty("name", IsRequired=true, IsKey=true)]
		public string Name
		{
			get
			{
				return base["name"] as string;
			}
			set
			{
				base["name"] = value;
			}
		}

		[ConfigurationProperty("typeName", IsRequired=true, IsKey=false)]
		public string TypeName
		{
			get
			{
				return base["typeName"] as string;
			}
			set
			{
				base["typeName"] = value;
			}
		}

		public HandlerElement()
		{
		}
	}
}