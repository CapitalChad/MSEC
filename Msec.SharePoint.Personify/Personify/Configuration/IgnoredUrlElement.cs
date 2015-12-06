using System;
using System.Configuration;

namespace Msec.Personify.Configuration {
	public sealed class IgnoredUrlElement : ConfigurationElement {
		public IgnoredUrlElement() : base() { }

		[ConfigurationProperty("isExactMatch", DefaultValue = false)]
		public Boolean IsExactMatch {
			get { return (Boolean)base["isExactMatch"]; }
			set { base["isExactMatch"] = value; }
		}
		[ConfigurationProperty("path", IsKey = true, IsRequired = true)]
		public String Path {
			get { return (String)base["path"]; }
			set { base["path"] = value; }
		}
	}
}
