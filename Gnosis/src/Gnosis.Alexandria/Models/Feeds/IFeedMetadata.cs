using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Models.Feeds
{
    public interface IFeedMetadata : IValue
    {
        IFeed Feed { get; }
        string Name { get; }
        string MediaType { get; }
        Uri Scheme { get; }
        string Content { get; }
    }
}
