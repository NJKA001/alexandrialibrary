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
        protected StateBase(IGame gameState)
            : this(gameState, Guid.NewGuid())
        {
        }

        protected StateBase(IGame gameState, Guid stateId)
        {
            if (gameState != null)
                AddState(gameState);

            this.stateId = stateId;
        }

        private readonly Guid stateId;
        private readonly IDictionary<Guid, IState> states = new Dictionary<Guid, IState>();

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void AddState(IState state)
        {
            if (states.ContainsKey(state.StateId))
                return;

            states.Add(state.StateId, state);
        }

        protected void RemoveState(IState state)
        {
            if (!states.ContainsKey(state.StateId))
                return;

            states.Remove(state.StateId);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Guid StateId
        {
            get { return stateId; }
        }

        public T GetState<T>(Guid stateId)
            where T : class, IState
        {
            return states.ContainsKey(stateId) ? states[stateId] as T : null;
        }

        public IEnumerable<T> GetStates<T>()
            where T : class, IState
        {
            return states.OfType<T>();
        }
    }
}
