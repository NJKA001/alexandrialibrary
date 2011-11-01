using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Text
{
    public interface ITextDocument
        : IDocument
    {
        string Body { get; }
    }
}
