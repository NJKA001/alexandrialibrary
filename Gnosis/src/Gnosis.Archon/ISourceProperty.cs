using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Archon
{
    public interface ISourceProperty
    {
        Guid Id { get; }
        ISource Source { get; }
        string Name { get; }
        Type Type { get; }
        object Default { get; }
        object Value { get; set; }
        bool IsValid(object value);
    }
}
