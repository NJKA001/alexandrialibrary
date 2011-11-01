using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Xspf
{
    public interface IXspfAlbum
        : IXspfElement
    {
        string Content { get; }
    }
}
