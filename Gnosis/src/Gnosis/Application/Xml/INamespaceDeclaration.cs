using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml
{
    public interface INamespaceDeclaration
        : IAttribute
    {
        string Alias { get; }
        Uri Identifier { get; }
    }
}
