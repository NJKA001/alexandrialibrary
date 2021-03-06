﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Namespaces.OpenSearch
{
    public class OpenSearchTotalResults
        : Element, IOpenSearchTotalResults
    {
        public OpenSearchTotalResults(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        #region ITotalResults Members

        public int Content
        {
            get { return GetContentInt32(0); }
        }

        #endregion
    }
}
