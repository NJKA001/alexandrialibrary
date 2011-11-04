using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models
{
    public interface ISourcePropertyFactory
    {
        ISourceProperty Create(ISource source, string name);
        ISourceProperty Create(Guid id, ISource source, string name);
    }
}
