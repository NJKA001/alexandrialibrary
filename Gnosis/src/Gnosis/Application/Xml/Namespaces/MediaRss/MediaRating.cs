using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Namespaces.MediaRss
{
    public class MediaRating
        : Element, IMediaRating
    {
        public MediaRating(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        #region IMediaRating Members

        public Uri Scheme
        {
            get { return GetAttributeUri("scheme"); }
        }

        public string Content
        {
            get { return GetContentString(); }
        }

        #endregion
    }
}
