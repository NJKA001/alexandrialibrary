using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.ViewModels
{
    public interface IAudioViewModel
    {
        int Number { get; }
        string Title { get; }
        string Duration { get; }
        IImage Image { get; }
    }
}
