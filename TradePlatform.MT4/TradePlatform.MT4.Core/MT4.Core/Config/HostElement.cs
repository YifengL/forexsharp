using System;
using System.Configuration;
using System.Net;

namespace TradePlatform.MT4.Core.Config
{
	public class HostElement : ConfigurationElement
	{
		[ConfigurationProperty("Handlers")]
		public HandlerElementCollection Handlers
		{
			get
			{
				return base["Handlers"] as HandlerElementCollection;
			}
		}

		[ConfigurationProperty("ipAddress", IsRequired=true, IsKey=false)]
		public string ipAddress
		{
			get
			{
				return base["ipAddress"] as string;
			}
			set
			{
				base["ipAddress"] = value;
			}
		}

		public IPAddress IPAddress
		{
			get
			{
				return IPAddress.Parse(this.ipAddress);
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

		[ConfigurationProperty("port", IsRequired=true, IsKey=false, DefaultValue=2007)]
		[IntegerValidator(MinValue=2000, MaxValue=3000)]
		public int Port
		{
			get
			{
				return (int)base["port"];
			}
			set
			{
				base["port"] = value;
			}
		}

		public HostElement()
		{
		}
	}
}