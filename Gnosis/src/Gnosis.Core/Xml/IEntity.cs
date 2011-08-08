using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml
{
    public interface IEntity
        : INode
    {
        string EntityName { get; }
        EntityType Type { get; }
        EntityVisibility Visibility { get; }
        string FormalPublicIdentifer { get; }
        string EntityValue { get; }
        string NData { get; }
    }
}
