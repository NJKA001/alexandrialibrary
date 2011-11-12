using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace Gnosis.Alexandria.ViewModels
{
    public abstract class TaskViewModel
        : ITaskViewModel, INotifyPropertyChanged
    {
        protected TaskViewModel(ILogger logger, ITask task, string name, object startingIcon)
            : this(logger, task, name, startingIcon, startingIcon, false)
        {
        }

        protected TaskViewModel(ILogger logger, ITask task, string name, object startingIcon, object completedIcon)
            : this(logger, task, name, startingIcon, completedIcon, false)
        {
        }

        protected TaskViewModel(ILogger logger, ITask task, string name, object startingIcon, object completedIcon, bool showElapsed)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");
            if (task == null)
                throw new ArgumentNullException("task");
            if (name == null)
                throw new ArgumentNullException("name");
            if (startingIcon == null)
                throw new ArgumentNullException("startingIcon");
            if (completedIcon == null)
                throw new ArgumentNullException("completedIcon");

            this.logger = logger;
            this.task = task;
            this.name = name;
            this.startingIcon = startingIcon;
            this.completedIcon = completedIcon;
            this.itemCount = task.Items.Count();
            this.showElapsed = showElapsed;

            task.AddCancelledCallback(() => OnCancelled());
            task.AddCompletedCallback(() => OnCompleted());
            task.AddErrorCallback(error => OnError(error));
            task.AddFailedCallback(error => OnFailed(error));
            task.AddPausedCallback(() => OnPaused());
            task.AddProgressCallback(progress => OnProgressChanged(progress));
            task.AddResumedCallback(() => OnResumed());
            task.AddStartedCallback(() => OnStarted());
        }

        private readonly ILogger logger;
        private readonly ITask task;
        private readonly string name;
        private readonly object startingIcon;
        private readonly object completedIcon;
        private readonly bool showElapsed;
        private readonly IList<Action<ITaskViewModel>> cancelCallbacks = new List<Action<ITaskViewModel>>();

        private string lastProgress;
        private string lastError;
        private Exception lastException;
        private int progressCount = 0;
        private int progressMaximum = 100;
        private int errorCount;
        private int itemCount;

        private bool isSelected;
        private bool isCancelled;

        private void OnCancelled()
        {
            try
            {
                OnStatusChanged();
                OnPropertyChanged("IsCancelled");
                foreach (var callback in cancelCallbacks)
                    callback(this);
            }
            catch (Exception ex)
            {
                logger.Error("  OnCancelled", ex);
            }
        }

        private void OnCompleted()
        {
            try
            {
                OnStatusChanged();
            }
            catch (Exception ex)
            {
                logger.Error("  OnCompleted", ex);
            }
        }

        private void OnProgressChanged(TaskProgress progress)
        {
            try
            {
                LastProgress = progress.Description;
                ProgressCount = progress.Count;
                ProgressMaximum = progress.Maximum;
                System.Diagnostics.Debug.WriteLine("progress: {0:000}/{1:000} {2}", progress.Count, progress.Maximum, progress.Description);
            }
            catch (Exception ex)
            {
                logger.Error("  OnProgressChanged", ex);
            }
        }

        private void OnError(Exception error)
        {
            try
            {
                LastError = error.Message;
                ErrorCount += 1;
                lastException = error;
            }
            catch (Exception ex)
            {
                logger.Error("  OnError", ex);
            }
        }

        private void OnFailed(Exception error)
        {
            try
            {
                OnError(error);
                OnStatusChanged();
            }
            catch (Exception ex)
            {
                logger.Error("  OnFailed", ex);
            }
        }

        private void OnStarted()
        {
            try
            {
                OnStatusChanged();
            }
            catch (Exception ex)
            {
                logger.Error("  OnStarted", ex);
            }
        }

        private void OnPaused()
        {
            try
            {
                OnStatusChanged();
            }
            catch (Exception ex)
            {
                logger.Error("  OnPaused", ex);
            }
        }

        private void OnResumed()
        {
            try
            {
                OnStatusChanged();
            }
            catch (Exception ex)
            {
                logger.Error("  OnResumed", ex);
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnStatusChanged()
        {
            OnPropertyChanged("Status");
            OnPropertyChanged("StatusName");
            OnPropertyChanged("Icon");
            OnPropertyChanged("RunningVisibility");
            OnPropertyChanged("StartVisibility");
            OnPropertyChanged("PauseVisibility");
            OnPropertyChanged("StopVisibility");
            OnPropertyChanged("PreviousVisibility");
            OnPropertyChanged("NextVisibility");
        }

        public string Name
        {
            get { return name; }
        }

        public object Icon
        {
            get { return task.Status == TaskStatus.Completed ? completedIcon : startingIcon; }
        }

        public TaskStatus Status
        {
            get { return task.Status; }
        }

        public string StatusName
        {
            get { return task.Status.ToString(); }
        }

        public ITaskItem CurrentItem
        {
            get { return task.CurrentItem; }
        }

        public bool SupportsPlayback
        {
            get { return task.SupportsPlayback; }
        }

        public string LastError
        {
            get { return lastError; }
            private set
            {
                lastError = value.ElideString(20);

                OnPropertyChanged("LastError");
            }
        }

        public string LastProgress
        {
            get { return lastProgress; }
            private set
            {
                lastProgress = value.ElideString(20);

                OnPropertyChanged("LastProgress");
            }
        }

        public int ErrorCount
        {
            get { return errorCount; }
            private set
            {
                errorCount = value;

                OnPropertyChanged("ErrorCount");
                OnPropertyChanged("ErrorVisibility");
            }
        }

        public int ProgressCount
        {
            get { return progressCount; }
            private set
            {
                progressCount = value;

                OnPropertyChanged("ProgressCount");
                OnPropertyChanged("ProgressVisibility");
            }
        }

        public int ProgressMaximum
        {
            get { return progressMaximum; }
            set
            {
                if (progressMaximum != value)
                {
                    progressMaximum = value;

                    OnPropertyChanged("ProgressMaximum");
                }
            }
        }

        public Visibility RunningVisibility
        {
            get
            {
                return (task.Status == TaskStatus.Running) ?
                    Visibility.Visible
                    : Visibility.Collapsed;
            }
        }

        public Visibility ErrorVisibility
        {
            get
            {
                return (errorCount > 0 || lastError != null) ?
                    Visibility.Visible
                    : Visibility.Collapsed;
            }
        }

        public Visibility ProgressVisibility
        {
            get
            {
                return !showElapsed ?
                    Visibility.Visible
                    : Visibility.Collapsed;
            }
        }

        public Visibility ElapsedVisibility
        {
            get
            {
                return showElapsed ?
                    Visibility.Visible
                    : Visibility.Collapsed;
            }
        }

        public Visibility StartVisibility
        {
            get 
            {
                return task.Status == TaskStatus.Running ? 
                    Visibility.Collapsed 
                    : Visibility.Visible;
            }
        }

        public Visibility PauseVisibility
        {
            get
            {
                return task.Status == TaskStatus.Running ?
                    Visibility.Visible 
                    : Visibility.Collapsed; 
            }
        }

        public Visibility StopVisibility
        {
            get
            {
                return (task.Status == TaskStatus.Running || task.Status == TaskStatus.Paused) ?
                    Visibility.Visible 
                    : Visibility.Collapsed;
            }
        }

        public Visibility PreviousVisibility
        {
            get
            {
                return itemCount > 1 ?
                    Visibility.Visible
                    : Visibility.Collapsed;
            }
        }

        public Visibility NextVisibility
        {
            get
            {
                return itemCount > 1 ?
                    Visibility.Visible
                    : Visibility.Collapsed;
            }
        }

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        public bool IsCancelled
        {
            get { return isCancelled; }
            set
            {
                if (!isCancelled && value)
                {
                    isCancelled = true;
                    Cancel();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Reset()
        {
            task.Reset();
        }

        public void Start()
        {
            task.Start();
        }

        public void Pause()
        {
            task.Pause();
        }

        public void Resume()
        {
            task.Resume();
        }

        public void Cancel()
        {
            task.Cancel();
        }

        public void Previous()
        {
            task.PreviousItem();
        }

        public void Next()
        {
            task.NextItem();
        }

        public void AddCancelCallback(Action<ITaskViewModel> callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            cancelCallbacks.Add(callback);
        }
    }
}
