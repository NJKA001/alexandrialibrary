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
        Guid Id { get; }
        string Name { get; }
        string Description { get; }
        object Icon { get; }
        TaskStatus Status { get; }
        string StatusName { get; }
        TaskItem CurrentItem { get; }
        string CurrentItemName { get; }
        int ErrorCount { get; }
        int ProgressCount { get; }
        int ProgressMaximum { get; }

        Visibility RunningVisibility { get; }
        Visibility ElapsedVisibility { get; }

        Visibility StartVisibility { get; }
        Visibility PauseVisibility { get; }
        Visibility StopVisibility { get; }
        Visibility PreviousVisibility { get; }
        Visibility NextVisibility { get; }
        
        bool IsSelected { get; set; }
        bool IsCancelled { get; set; }

        void Reset();
        void Start();
        void Stop();
        void Pause();
        void Resume();
        void Cancel();
        void Previous();
        void Next();

        void BeginProgressUpdate();
        void UpdateProgress(int value);

        void AddStartedCallback(Action<ITaskViewModel> callback);
        void AddCancelCallback(Action<ITaskViewModel> callback);

        void AddItemChangedCallback(Action<TaskItem> callback);
        void AddProgressCallback(Action<TaskProgress> callback);
        void AddErrorCallback(Action<TaskError> callback);
    }

    public interface ITaskViewModel<T>
        : ITaskViewModel
    {
        void AddResultsCallback(Action<T> callback);
    }
}
