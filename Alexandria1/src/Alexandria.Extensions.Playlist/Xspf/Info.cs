using System;
using System.Collections.Generic;
using System.Xml;

namespace Alexandria.Extensions.Playlist.Xspf
{
	public struct Info
	{
		public Info(XmlNode node)
		{
			value = new Uri(node.InnerText);
		}
		
		public Info(Uri value)
		{
			this.value = value;
		}
		
		private Uri value;
		
		public Uri Value
		{
			get { return value; }
		}

		public override string ToString()
		{
			return (Value != null) ? Value.ToString() : string.Empty;
		}
	}
}
