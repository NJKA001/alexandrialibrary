using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.FeedBurner
{
    public class FeedBurnerInfo
        : Element, IFeedBurnerInfo
    {
        public FeedBurnerInfo(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        #region IFeedBurnerInfo Members

        public Uri Uri
        {
            get { return GetAttributeUri("uri"); }
        }

        #endregion
    }
}
