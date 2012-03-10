using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Data
{
    public interface ILink
        : IEntity
    {
        uint Parent { get; set; }
        EntityType ParentType { get; set; }
        string Name { get; set; }
        Relationship Relationship { get; set; }
        string Target { get; set; }
        TargetType TargetType { get; set; }
    }
}
