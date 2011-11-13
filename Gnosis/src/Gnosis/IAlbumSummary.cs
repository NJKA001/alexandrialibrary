using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IAlbumSummary
    {
        string Title { get; }
        DateTime Date { get; }
        Uri Image { get; }
    }
}
