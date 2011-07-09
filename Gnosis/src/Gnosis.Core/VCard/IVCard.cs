using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.VCard
{
    /// <summary>
    /// Defines a vCard based on IETF standard RFC 2426 
    /// </summary>
    public interface IVCard
    {
        string Version { get; }
        string N { get; }
        string FN { get; }
        string Org { get; }
        string Title { get; }
        DateTime Rev { get; }
    }
}
