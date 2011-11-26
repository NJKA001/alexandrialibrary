using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface ILink
    {
        long Id { get; }
        Uri Source { get; }
        Uri Target { get; }
        string Relationship { get; }
        string Name { get; }
    }
}
