using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Feeds
{
    public interface IFeedCategory : IValue
    {
        IFeed Feed { get; }
        string Name { get; }
        Uri Scheme { get; }
        string Label { get; }
    }
}
