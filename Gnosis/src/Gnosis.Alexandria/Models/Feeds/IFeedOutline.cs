using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Data;

namespace Gnosis.Alexandria.Models.Feeds
{
    public interface IFeedOutline
        : IOutline<IFeed>
    {
        Uri Location { get; set; }
        string Title { get; set; }
        string Authors { get; set; }
        string Description { get; set; }
        Uri ImagePath { get; set; }
        Uri IconPath { get; set; }
    }
}
