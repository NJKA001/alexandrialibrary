using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace LotR.Client.ViewModels
{
    public class StatusViewModel
        : ViewModelBase
    {
        private string currentStatus = string.Empty;
        private readonly ObservableCollection<string> history = new ObservableCollection<string>();

        public string CurrentStatus
        {
            get { return currentStatus; }
            private set
            {
                if (currentStatus == value)
                    return;

                currentStatus = value;
                OnPropertyChanged("CurrentStatus");
            }
        }

        public IEnumerable<string> History
        {
            get { return history; }
        }

        public void SetCurrentStatus(string status)
        {
            if (status == null)
                throw new ArgumentNullException("status");

            history.Add(status);
            CurrentStatus = status;
        }
    }
}
