using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Document.Xml.OpenSearch
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
