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
        protected StateBase(IGame game)
            : this(game, Guid.NewGuid())
        {
        }

        protected StateBase(IGame game, Guid stateId)
        {
            this.game = game;
            this.stateId = stateId;
        }

        private readonly Guid stateId;
        private readonly IGame game;
        
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public Guid StateId
        {
            get { return stateId; }
        }

        public IGame Game
        {
            get { return game; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
