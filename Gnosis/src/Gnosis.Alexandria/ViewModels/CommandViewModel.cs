using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.ViewModels
{
    public class CommandViewModel
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
