using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface ITagNamespace
    {
        long Id { get; }
        string Name { get; }
        Uri Uri { get; }
        string Version { get; }
        int SearchWeight { get; }
    }
}
