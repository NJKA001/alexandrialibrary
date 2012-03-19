using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml
{
    public interface IProcessingInstruction
        : INode
    {
        string Target { get; }
        string Content { get; }
    }
}
