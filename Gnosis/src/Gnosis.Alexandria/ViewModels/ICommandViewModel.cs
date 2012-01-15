using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.ViewModels
{
    public interface ICommandViewModel
        : INotifyPropertyChanged
    {
        string Name { get; }
        string Description { get; }
        object Icon { get; }

        bool IsSelected { get; set; }
    }
}
