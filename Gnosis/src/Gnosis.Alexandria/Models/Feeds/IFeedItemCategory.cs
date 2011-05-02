using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Feeds
{
    public interface IFeedItemCategory : IModel
    {
        IFeed Feed { get; }
        string Name { get; }
        Uri Scheme { get; }
        string Label { get; }
    }
}
