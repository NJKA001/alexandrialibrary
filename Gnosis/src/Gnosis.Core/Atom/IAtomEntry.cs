using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Atom
{
    public interface IAtomEntry
    {
        IAtomTitle Title { get; }
        IAtomLink Link { get; }
        IAtomId Id { get; }
        IAtomUpdated Updated { get; }
        IAtomSummary Summary { get; }
        IAtomContent Content { get; }
    }
}
