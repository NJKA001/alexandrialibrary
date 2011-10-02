using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface ILink
    {
        long Id { get; }
        Uri Source { get; }
        Uri Target { get; }
        ISchema Schema { get; }
        string Name { get; }
    }
}
