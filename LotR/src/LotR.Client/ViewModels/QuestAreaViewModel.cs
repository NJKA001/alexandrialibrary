using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Threading;

using LotR.States;

namespace LotR.Client.ViewModels
{
    public class QuestAreaViewModel
        : ViewModelBase
    {
        public QuestAreaViewModel(Dispatcher dispatcher, IGame game)
            : base(dispatcher)
        {
            if (game == null)
                throw new ArgumentNullException("game");

            if (game.QuestArea == null)
                throw new InvalidOperationException("Game is not initialized, QuestArea is undefined");

            this.game = game;
            this.game.QuestArea.PropertyChanged += (sender, args) => QuestAreaPropertyChanged(sender, args);

            RefreshActiveLocation();
            RefreshActiveQuest();
        }

        private readonly IGame game;
        private EncounterCardInPlayViewModel activeLocation;
        private QuestCardInPlayViewModel activeQuest;

        private void QuestAreaPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            switch (args.PropertyName)
            {
                case "ActiveLocation":
                    RefreshActiveLocation();
                    break;
                case "ActiveQuest":
                    RefreshActiveQuest();
                    break;
                default:
                    break;
            }
        }

        private void RefreshActiveLocation()
        {
            if (game.QuestArea.ActiveLocation == null)
            {
                ActiveLocation = null;
                return;
            }

            ActiveLocation = new EncounterCardInPlayViewModel(dispatcher, game.QuestArea.ActiveLocation as IEncounterInPlay);
        }

        private void RefreshActiveQuest()
        {
            if (game.QuestArea.ActiveQuest == null)
            {
                ActiveQuest = null;
                return;
            }

            ActiveQuest = new QuestCardInPlayViewModel(dispatcher, game.QuestArea.ActiveQuest);
        }

        public EncounterCardInPlayViewModel ActiveLocation
        {
            get { return activeLocation; }
            set
            {
                activeLocation = value;
                OnPropertyChanged("ActiveLocation");
                OnPropertyChanged("ActiveLocationVisibility");
            }
        }

        public Visibility ActiveLocationVisibility
        {
            get { return activeLocation != null ? Visibility.Visible : Visibility.Collapsed; }
        }

        public QuestCardInPlayViewModel ActiveQuest
        {
            get { return activeQuest; }
            set
            {
                activeQuest = value;
                OnPropertyChanged("ActiveQuest");
                OnPropertyChanged("ActiveQuestVisibility");
            }
        }

        public Visibility ActiveQuestVisibility
        {
            get { return activeQuest != null ? Visibility.Visible : Visibility.Collapsed; }
        }
    }
}
