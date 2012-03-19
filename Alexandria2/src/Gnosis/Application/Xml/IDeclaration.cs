using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml
{
    public interface IDeclaration
        : INode
    {
        string Version { get; }
        string Encoding { get; }
        Standalone Standalone { get; }
    }
}
