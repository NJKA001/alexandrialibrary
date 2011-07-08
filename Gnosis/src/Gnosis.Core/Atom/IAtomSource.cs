using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Atom
{
    public interface IAtomSource
        : IAtomCommon
    {
        IEnumerable<IAtomPerson> Authors { get; }
        IAtomId Id { get; }
        IEnumerable<IAtomLink> Links { get; }
        IAtomTitle Title { get; }
        IAtomUpdated Updated { get; }

        IEnumerable<IAtomCategory> Categories { get; }
        IEnumerable<IAtomPerson> Contributors { get; }
        IAtomGenerator Generator { get; }
        IAtomIcon Icon { get; }
        IAtomLogo Logo { get; }
        IAtomRights Rights { get; }
        IAtomSubtitle Subtitle { get; }
    }
}
