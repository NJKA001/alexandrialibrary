using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.ViewModels
{
    public interface IMediaMarqueeViewModel
        : INotifyPropertyChanged
    {
        Uri Location { get; }
        MediaCategory Category { get; }
        string Name { get; }
        string Subtitle { get; }

        bool IsSelected { get; set; }
    }
}
