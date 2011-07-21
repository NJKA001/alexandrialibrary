using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.W3c;

namespace Gnosis.Core.Xml
{
    public class XmlDeclaration
        : IXmlDeclaration
    {
        public XmlDeclaration(string version, ICharacterSet encoding, XmlStandalone standalone)
        {
            if (version == null)
                throw new ArgumentNullException("version");

            this.version = version;
            this.encoding = encoding;
            this.standalone = standalone;
        }

        private readonly string version;
        private readonly ICharacterSet encoding;
        private readonly XmlStandalone standalone;

        #region IXmlDeclaration Members

        public string Version
        {
            get { return version; }
        }

        public ICharacterSet Encoding
        {
            get { return encoding; }
        }

        public XmlStandalone Standalone
        {
            get { return standalone; }
        }

        #endregion

        public override string ToString()
        {
            var xml = new StringBuilder();
            xml.AppendFormat("<?xml version=\"{0}\"", version);

            if (encoding != null)
                xml.AppendFormat(" encoding=\"{0}\"", encoding);

            if (standalone != XmlStandalone.Undefined)
                xml.AppendFormat(" standalone=\"{0}\"", standalone.ToString().ToLower());

            xml.Append("?>");

            return xml.ToString();
        }
    }
}
