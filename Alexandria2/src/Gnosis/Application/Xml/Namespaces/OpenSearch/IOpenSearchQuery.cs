﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Culture;

namespace Gnosis.Application.Xml.Namespaces.OpenSearch
{
    public interface IOpenSearchQuery
        : IOpenSearchElement
    {
        string Role { get; }
        string Title { get; }
        int TotalResults { get; }
        string SearchTerms { get; }
        int Count { get; }
        int StartIndex { get; }
        int StartPage { get; }
        ILanguageTag Language { get; }
        ICharacterSet OutputEncoding { get; }
    }
}
