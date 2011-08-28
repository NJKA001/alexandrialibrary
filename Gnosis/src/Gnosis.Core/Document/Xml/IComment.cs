using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Document.Xml
{
    public interface IComment
        : IMarkup
    {
        string Content { get; }
    }
}
