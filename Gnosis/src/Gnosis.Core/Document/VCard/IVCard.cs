using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Document.VCard
{
    /// <summary>
    /// Defines a vCard based on version 3.0 of IETF RFC 2426 
    /// </summary>
    /// <remarks>http://www.ietf.org/rfc/rfc2426.txt</remarks>
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
