using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Namespaces.Google.Data
{
    public class GoogleDataFeedLink
        : Element, IGoogleDataFeedLink
    {
        public GoogleDataFeedLink(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        #region IGoogleDataFeedLink Members

        public string Rel
        {
            get { return GetAttributeString("rel"); }
        }

        public Uri Href
        {
            get { return GetAttributeUri("href"); }
        }

        public int CountHint
        {
            get { return GetAttributeInt32("countHint"); }
        }

        #endregion
    }
}
