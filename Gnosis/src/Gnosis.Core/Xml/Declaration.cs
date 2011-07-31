using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.W3c;

namespace Gnosis.Core.Xml
{
    public class Declaration
        : IDeclaration
    {
        public Declaration(string version, ICharacterSet encoding, Standalone standalone)
        {
            if (version == null)
                throw new ArgumentNullException("version");

            this.version = version;
            this.encoding = encoding;
            this.standalone = standalone;
        }

        private readonly string version;
        private readonly ICharacterSet encoding;
        private readonly Standalone standalone;

        #region IXmlDeclaration Members

        public string Version
        {
            get { return version; }
        }

        public ICharacterSet Encoding
        {
            get { return encoding; }
        }

        public Standalone Standalone
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

            if (standalone != Standalone.Undefined)
                xml.AppendFormat(" standalone=\"{0}\"", standalone.ToString().ToLower());

            xml.Append("?>");

            return xml.ToString();
        }
    }
}
