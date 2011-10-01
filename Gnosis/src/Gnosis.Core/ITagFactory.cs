using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface ITagFactory
    {
        ITag Create(Uri target, IAlgorithm algorithm, Uri type, string name);
        ITag Create(Uri target, IAlgorithm algorithm, Uri type, string name, long id);
    }
}
