using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.ViewModels
{
    public interface IAlbumViewModel
    {
        string Title { get; }
        string Year { get; }
        IImage Image { get; }
    }
}
