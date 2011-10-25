using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml
{
    public class QualifiedName
        : IQualifiedName
    {
        private QualifiedName(string localPart, string prefix)
        {
            if (localPart == null)
                throw new ArgumentNullException("localPart");

            this.localPart = localPart;
            this.prefix = prefix;
        }

        private readonly string localPart;
        private readonly string prefix;

        #region IXmlQualifiedName Members

        public string Prefix
        {
            get { return prefix; }
        }

        public string LocalPart
        {
            get { return localPart; }
        }

        #endregion

        public override string ToString()
        {
            return prefix != null ?
                string.Format("{0}:{1}", prefix, localPart)
                : localPart;
        }

        public static IQualifiedName Parse(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            string prefix = null;
            var localPart = name;

            if (name.Contains(':'))
            {
                var tokens = name.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                if (tokens != null && tokens.Length == 2)
                {
                    prefix = tokens[0];
                    localPart = tokens[1];
                }
            }

            return localPart != null ?
                new QualifiedName(localPart, prefix)
                : null;
        }
    }
}
