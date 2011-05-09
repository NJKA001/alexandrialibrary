using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Models.Feeds
{
    public interface IFeedLink : IValue
    {
        string Relationship { get; }
        Uri Location { get; }
        string MediaType { get; }
        uint Length { get; }
        string Language { get; }
    }
}
