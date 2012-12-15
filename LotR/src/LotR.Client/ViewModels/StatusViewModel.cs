using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Threading;

using LotR.States;

namespace LotR.Client.ViewModels
{
    public class StatusViewModel
        : ViewModelBase
    {
        public StatusViewModel(Dispatcher dispatcher, IGame game)
            : base(dispatcher)
        {
            this.game = game;
        }

        private readonly IGame game;
        private string currentStatus = string.Empty;
        private readonly ObservableCollection<string> history = new ObservableCollection<string>();

        public int CurrentRound
        {
            get { return game.CurrentRound; }
        }

        public string CurrentPhase
        {
            get { return game.CurrentPhase != null ? game.CurrentPhase.Name : "Setup"; }
        }

        public string CurrentStep
        {
            get { return game.CurrentPhase != null ? game.CurrentPhase.StepName : "Follow Scenario Instructions"; }
        }

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

            Dispatch(() => history.Add(status));
            CurrentStatus = status;
        }
    }
}
