using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.ViewModels
{
    public interface ITaskResultViewModel
        : INotifyPropertyChanged
    {
        Guid Id { get; }
        string Name { get; }
        string Description { get; }
        object Control { get; }

        bool IsSelected { get; set; }
        bool IsClosed { get; set; }

        void AddClosedCallback(Action<ITaskResultViewModel> callback);
    }
}
