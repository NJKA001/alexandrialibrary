using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface ILinkFactory
    {
        ILink Create(Uri source, Uri target, Uri type, string name);
        ILink Create(Uri source, Uri target, Uri type, string name, long id);
    }
}
