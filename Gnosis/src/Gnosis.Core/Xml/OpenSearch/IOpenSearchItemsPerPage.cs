﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.OpenSearch
{
    public interface IOpenSearchItemsPerPage
        : IOpenSearchElement
    {
        int Content { get; }
    }
}
