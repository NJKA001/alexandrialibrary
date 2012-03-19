using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml
{
    public interface ICharacterData
        : INode
    {
        string Content { get; }
    }
}
