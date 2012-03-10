using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Data
{
    public interface ITag
        : IEntity
    {
        uint Parent { get; set; }
        EntityType ParentType { get; set; }
        string Name { get; set; }
        Category Category { get; set; }
        Algorithm Algorithm { get; set; }
    }
}
