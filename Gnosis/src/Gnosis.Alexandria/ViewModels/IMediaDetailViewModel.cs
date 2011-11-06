using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.ViewModels
{
    public interface IMediaDetailViewModel
        : INotifyPropertyChanged
    {
        string Type { get; }
        string Value { get; }
        object Thumbnail { get; }

        bool IsSelected { get; set; }
    }
}
