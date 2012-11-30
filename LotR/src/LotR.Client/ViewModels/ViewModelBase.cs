using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace LotR.Client.ViewModels
{
    public abstract class ViewModelBase
        : INotifyPropertyChanged
    {
        protected ViewModelBase(Dispatcher dispatcher)
        {
            if (dispatcher == null)
                throw new ArgumentNullException("dispatcher");

            this.dispatcher = dispatcher;
        }

        protected readonly Dispatcher dispatcher;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void Dispatch(Action action)
        {
            if (action == null)
                throw new ArgumentNullException("action");

            dispatcher.Invoke(action, DispatcherPriority.DataBind);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
