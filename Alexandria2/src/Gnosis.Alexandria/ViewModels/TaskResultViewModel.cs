using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Gnosis.Alexandria.ViewModels
{
    public class TaskResultViewModel
        : ITaskResultViewModel
    {
        public TaskResultViewModel(ITaskViewModel taskViewModel, TabItem tabItem)
        {
            if (taskViewModel == null)
                throw new ArgumentNullException("taskViewModel");
            if (tabItem == null)
                throw new ArgumentNullException("tabItem");

            this.taskViewModel = taskViewModel;
            this.tabItem = tabItem;
            taskViewModel.AddCancelCallback(x => IsClosed = true);
        }

        private readonly ITaskViewModel taskViewModel;
        private readonly TabItem tabItem;
        private readonly IList<Action<ITaskResultViewModel>> closeCallbacks = new List<Action<ITaskResultViewModel>>();

        private bool isSelected;
        private bool isClosed;

        private void OnClosed()
        {
            foreach (var callback in closeCallbacks)
                callback(this);
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public Guid Id
        {
            get { return taskViewModel.Id; }
        }

        public string Name
        {
            get { return taskViewModel.Name; }
        }

        public string Description
        {
            get { return taskViewModel.Description; }
        }

        public object Control
        {
            get { return tabItem; }
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

        public bool IsClosed
        {
            get { return isClosed; }
            set
            {
                if (!isClosed && value)
                {
                    //NOTE: This code cancels and closes the associated task
                    //if (!taskViewModel.IsCancelled)
                    //{
                    //    taskViewModel.IsCancelled = true;

                    //    if (!taskViewModel.IsCancelled)
                    //        return;
                    //}
                    
                    isClosed = value;
                    OnPropertyChanged("IsClosed");
                    OnClosed();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void AddClosedCallback(Action<ITaskResultViewModel> callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            closeCallbacks.Add(callback);
        }
    }
}
