using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Controllers;
using Gnosis.Alexandria.Views;

namespace Gnosis.Alexandria.ViewModels
{
    public interface ICommandViewModel
        : INotifyPropertyChanged
    {
        string Name { get; }
        string Description { get; }
        object Icon { get; }

        bool IsSelected { get; set; }

        void Execute(ITaskController taskController, TaskResultView taskResultView);
    }
}
