using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Image;

namespace Gnosis.Core
{
    public interface IMediaDetail
    {
        ITag Tag { get; }
        IImage Thumbnail { get; }
    }
}
