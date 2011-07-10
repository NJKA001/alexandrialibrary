using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.W3c
{
    public class XmlStyleSheet
        : IXmlStyleSheet
    {
        public XmlStyleSheet(IMediaType type, IMedia media, Uri href)
        {
            this.type = type;
            this.media = media;
            this.href = href;
        }

        private readonly IMediaType type;
        private readonly IMedia media;
        private readonly Uri href;

        #region IXmlStyleSheet Members

        public IMediaType Type
        {
            get { return type; }
        }

        public IMedia Media
        {
            get { return media; }
        }

        public Uri Href
        {
            get { return href; }
        }

        #endregion

        public override string ToString()
        {
            var xml = new StringBuilder();

            xml.Append("<?xml-stylesheet");

            if (href != null)
                xml.AppendFormat(" href='{0}'", href.ToString());

            if (type != null)
                xml.AppendFormat(" type='{0}'", type.ToString());

            if (media != null)
                xml.AppendFormat(" media='{0}'", media.ToString());

            xml.Append("?>");

            return xml.ToString();
        }
    }
}
