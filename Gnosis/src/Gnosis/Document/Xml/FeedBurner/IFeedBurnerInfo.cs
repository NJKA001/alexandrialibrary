using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml.FeedBurner
{
    public interface IFeedBurnerInfo
        : IFeedBurnerElement
    {
        Uri Uri { get; }
    }
}
