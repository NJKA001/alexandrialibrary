using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace LotR.States
{
    public class StateBase
        : IState, INotifyPropertyChanged
    {
        private readonly IList<IState> states = new List<IState>();

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void AddState(IState state)
        {
            states.Add(state);
        }

        protected void RemoveState(IState state)
        {
            if (!states.Contains(state))
                return;

            states.Remove(state);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerable<T> GetStates<T>() where T : IState
        {
            return states.OfType<T>();
        }
    }
}
