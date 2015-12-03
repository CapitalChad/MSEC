using System;
using System.Configuration;

namespace Msec.Personify.Configuration {
	public sealed class IgnoredUrlsElementCollection : ConfigurationElementCollection {
		public IgnoredUrlsElementCollection() : base() { }

		protected override ConfigurationElement CreateNewElement() {
			return new IgnoredUrlElement();
		}
		protected override Object GetElementKey(ConfigurationElement element) {
			IgnoredUrlElement ignoredUrlElement = (IgnoredUrlElement)element;
			return ignoredUrlElement.Path;
		}
	}
}
