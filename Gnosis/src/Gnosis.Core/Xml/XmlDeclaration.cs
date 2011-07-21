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
        public XmlDeclaration(string version, ICharacterSet encoding, bool standalone)
        {
            if (version == null)
                throw new ArgumentNullException("version");
            if (encoding == null)
                throw new ArgumentNullException("encoding");

            this.version = version;
            this.encoding = encoding;
            this.standalone = standalone;
        }

        private readonly string version;
        private readonly ICharacterSet encoding;
        private readonly bool standalone;


        #region IXmlDeclaration Members

        public string Version
        {
            get { return version; }
        }

        public ICharacterSet Encoding
        {
            get { return encoding; }
        }

        public bool Standalone
        {
            get { return standalone; }
        }

        #endregion

        public override string ToString()
        {
            return string.Format("<?xml version=\"{0}\" encoding=\"{1}\" standalone=\"{2}\" ?>", version, encoding, standalone);
        }
    }
}
