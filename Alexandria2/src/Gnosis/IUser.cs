using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IUser
        : IMedia
    {
        string Name { get; }
        Uri Thumbnail { get; }
    }
}
