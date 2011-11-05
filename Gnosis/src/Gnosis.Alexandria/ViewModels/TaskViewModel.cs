using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.ViewModels
{
    public class TaskViewModel
        : ITaskViewModel, INotifyPropertyChanged
    {
        public TaskViewModel(string name)
        {
            this.name = name;
            this.imageSource = "pack://application:,,,/Images/Gear-01.png";
        }

        public TaskViewModel(string name, object imageSource)
        {
            this.name = name;
            this.imageSource = imageSource;
        }

        private readonly string name;
        private readonly object imageSource;
        private readonly object statusIconSource = "pack://application:,,,/Images/throbber20.gif";
        private string progressDescription = "Doing some work...";
        
        private bool isSelected;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Name
        {
            get { return name; }
        }

        public object ImageSource
        {
            get { return imageSource; }
        }

        public object StatusIconSource
        {
            get { return statusIconSource; }
        }

        public string ProgressDescription
        {
            get { return progressDescription; }
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

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
