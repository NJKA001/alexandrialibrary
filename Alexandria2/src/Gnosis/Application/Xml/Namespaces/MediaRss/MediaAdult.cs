using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Namespaces.MediaRss
{
    public class MediaAdult
        : Element, IMediaAdult
    {
        public MediaAdult(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        #region IMediaAdult Members

        public bool IsAdult
        {
            get { return GetContentBoolean(false); }
        }

        #endregion
    }
}
