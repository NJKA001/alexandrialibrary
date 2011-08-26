using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.Atom
{
    public interface IAtomEntry
        : IAtomCommon
    {
        IEnumerable<IAtomAuthor> Authors { get; }
        IAtomContent Content { get; }
        IAtomId Id { get; }
        IEnumerable<IAtomLink> Links { get; }
        IAtomSummary Summary { get; }
        IAtomTitle Title { get; }
        IAtomUpdated Updated { get; }

        IEnumerable<IAtomCategory> Categories { get; }
        IEnumerable<IAtomContributor> Contributors { get; }
        IAtomPublished Published { get; }
        IAtomRights Rights { get; }
        IAtomSource Source { get; }
    }
}
