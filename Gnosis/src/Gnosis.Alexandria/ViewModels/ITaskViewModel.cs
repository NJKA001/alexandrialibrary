using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Gnosis.Alexandria.ViewModels
{
    public interface ITaskViewModel
        : INotifyPropertyChanged
    {
        string Name { get; }
        object Icon { get; }
        TaskStatus Status { get; }
        string LastError { get; }
        string LastProgress { get; }
        int ErrorCount { get; }
        int ProgressCount { get; }

        Visibility RunningVisibility { get; }
        Visibility ErrorVisibility { get; }
        Visibility ProgressVisibility { get; }

        Visibility StartVisibility { get; }
        Visibility PauseVisibility { get; }
        Visibility StopVisibility { get; }
        Visibility PreviousVisibility { get; }
        Visibility NextVisibility { get; }
        
        bool IsSelected { get; set; }

        void Reset();
        void Start();
        void Pause();
        void Resume();
        void Cancel();
    }
}
