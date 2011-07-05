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
    }
}
