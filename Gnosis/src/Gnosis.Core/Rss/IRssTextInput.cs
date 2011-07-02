using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Rss
{
    public interface IRssTextInput
    {
        string Title { get; }
        string Description { get; }
        string Name { get; }
        Uri Link { get; }
    }
}
