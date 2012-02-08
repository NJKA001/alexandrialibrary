using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Gnosis.Data;

namespace Gnosis.Alexandria.Models.Feeds
{
    public interface IFeedCategory : IValue
    {
        Uri Scheme { get; }
        string Name { get; }
        string Label { get; }
    }
}
