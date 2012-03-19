using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.ViewModels
{
    public interface ILinkViewModel
        : INotifyPropertyChanged
    {
        long Id { get; }
        string Name { get; }
        string Relationship { get; }
        Uri Source { get; }
        Uri Target { get; }

        bool IsClosed { get; set; }
        bool IsSelected { get; set; }
    }
}
