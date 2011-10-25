using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml
{
    public interface INamespaceDeclaration
        : IAttribute
    {
        string Alias { get; }
        Uri Identifier { get; }
    }
}
