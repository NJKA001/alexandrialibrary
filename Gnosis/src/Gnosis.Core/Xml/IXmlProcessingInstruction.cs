using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml
{
    public interface IXmlProcessingInstruction
    {
        string Target { get; }
        string Content { get; }
    }
}
