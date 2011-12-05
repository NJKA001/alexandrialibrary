using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.ViewModels
{
    public interface IMediaItemViewModel
        : INotifyPropertyChanged
    {
        bool IsSelected { get; set; }
    }
}
