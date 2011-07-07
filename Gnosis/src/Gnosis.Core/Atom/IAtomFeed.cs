using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.W3c;

namespace Gnosis.Core.Atom
{
    /// <summary>
    /// Defines an Atom 1.0 Feed based on IETF RFC 4287
    /// </summary>
    /// <remarks>http://tools.ietf.org/html/rfc4287</remarks>
    public interface IAtomFeed
        : IAtomCommon, IXmlDocument
    {
        IEnumerable<IAtomPerson> Authors { get; }
        IAtomId Id { get; }
        IEnumerable<IAtomLink> Links { get; }
        IAtomTitle Title { get; }
        IAtomUpdated Updated { get; }

        IEnumerable<IAtomCategory> Categories { get; }
        IEnumerable<IAtomPerson> Contributors { get; }
        IEnumerable<IAtomEntry> Entries { get; }
        IAtomGenerator Generator { get; }
        IAtomIcon Icon { get; }
        IAtomLogo Logo { get; }
        IAtomRights Rights { get; }
        IAtomSubtitle Subtitle { get; }
        IEnumerable<IAtomExtension> Extensions { get; }
    }
}
