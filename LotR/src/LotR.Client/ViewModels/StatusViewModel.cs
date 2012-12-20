using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
            if (game == null)
                throw new ArgumentNullException("game");

            this.game = game;
            game.PropertyChanged += (sender, args) => GamePropertyChanged(sender, args);
            RegisterCurrentPhaseNotification();
        }

        private readonly IGame game;
        private string currentStatus = string.Empty;
        private readonly ObservableCollection<string> history = new ObservableCollection<string>();

        private void RegisterCurrentPhaseNotification()
        {
            if (game.CurrentPhase == null)
                return;

            game.CurrentPhase.PropertyChanged += (sender, args) => PhasePropertyChanged(sender, args);
        }

        private void GamePropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            switch (args.PropertyName)
            {
                case "CurrentRound":
                    OnPropertyChanged("CurrentRound");
                    break;
                case "CurrentPhase":
                    OnPropertyChanged("CurrentPhase");
                    OnPropertyChanged("CurrentStep");
                    RegisterCurrentPhaseNotification();
                    break;
                default:
                    break;
            }
        }

        private void PhasePropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            switch (args.PropertyName)
            {
                case "StepName":
                    OnPropertyChanged("CurrentStep");
                    break;
                default:
                    break;
            }
        }

        public uint CurrentRound
        {
            get { return game.CurrentRound; }
        }

        public string CurrentPhase
        {
            get { return game.CurrentPhase != null ? game.CurrentPhase.Name : "Setup"; }
        }

        public string CurrentStep
        {
            get { return game.CurrentPhase != null ? game.CurrentPhase.StepName : CurrentStatus; }
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
                OnPropertyChanged("CurrentStep");
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
