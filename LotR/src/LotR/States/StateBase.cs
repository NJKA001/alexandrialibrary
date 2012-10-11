using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace LotR.States
{
    public abstract class StateBase
        : IState, INotifyPropertyChanged
    {
        protected StateBase()
        {
            this.stateId = Guid.NewGuid();
        }

        protected StateBase(Guid stateId)
        {
            this.stateId = stateId;
        }

        private readonly Guid stateId;
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

        public Guid StateId
        {
            get { return stateId; }
        }

        public T GetState<T>(Guid stateId)
            where T : IState
        {
            return states.OfType<T>().Where(x => x.StateId == stateId).FirstOrDefault();
        }

        public IEnumerable<T> GetStates<T>()
            where T : IState
        {
            return states.OfType<T>();
        }
    }
}
