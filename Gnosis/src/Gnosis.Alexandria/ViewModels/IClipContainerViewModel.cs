using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.ViewModels
{
    public interface IClipContainerViewModel
    {
        IEnumerable<IClipViewModel> Clips { get; }

        void AddClip(IClipViewModel clip);
        void RemoveClip(IClipViewModel clip);
    }
}
