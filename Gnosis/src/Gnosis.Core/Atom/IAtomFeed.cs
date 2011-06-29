using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Atom
{
    /// <summary>
    /// Defines an Atom 1.0 Feed based on IETF RFC 4287
    /// </summary>
    /// <remarks>http://tools.ietf.org/html/rfc4287</remarks>
    public interface IAtomFeed
    {
        string Title { get; }
        DateTime Updated { get; }
        IAtomPerson Author { get; }
        Uri Id { get; }

        IEnumerable<IAtomEntry> Entries { get; }
    }
}
