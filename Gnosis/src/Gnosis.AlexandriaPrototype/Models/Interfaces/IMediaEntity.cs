using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Babel;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface IMediaEntity : IModel
    {
        string Type { get; set; }
        string Marquee { get; set; }
        string Hash { get; set; }
        string By { get; set; }
        string On { get; set; }
        string From { get; set; }
    }
}
