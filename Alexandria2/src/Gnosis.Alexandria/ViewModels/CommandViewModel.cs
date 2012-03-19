using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Controllers;
using Gnosis.Alexandria.Views;

namespace Gnosis.Alexandria.ViewModels
{
    public abstract class CommandViewModel
        : ICommandViewModel
    {
        public CommandViewModel(string name, string description, object icon)
        {
            this.name = name;
            this.description = description;
            this.icon = icon;
        }

        private string name;
        private string description;
        private object icon;

        private bool isSelected;

        protected abstract void DoExecute(ITaskController taskController, TaskResultView taskResultView);

        public string Name
        {
            get { return name; }
        }

        public string Description
        {
            get { return description; }
        }

        public object Icon
        {
            get { return icon; }
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

        public void Execute(ITaskController taskController, TaskResultView taskResultView)
        {
            if (taskController == null)
                throw new ArgumentNullException("taskController");
            if (taskResultView == null)
                throw new ArgumentNullException("taskResultView");

            DoExecute(taskController, taskResultView);
        }

        #region INotifyPropertyChanged Members

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
