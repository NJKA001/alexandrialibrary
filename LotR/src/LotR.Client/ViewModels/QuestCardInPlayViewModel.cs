using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Threading;

using LotR.States;

namespace LotR.Client.ViewModels
{
    public class QuestCardInPlayViewModel
        : QuestCardViewModel
    {
        public QuestCardInPlayViewModel(Dispatcher dispatcher, IQuestInPlay questInPlay)
            : base(dispatcher, questInPlay.Card)
        {
            if (questInPlay == null)
                throw new ArgumentNullException("questInPlay");

            this.questInPlay = questInPlay;
        }

        private readonly IQuestInPlay questInPlay;

        private void ModelPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            switch (args.PropertyName)
            {
                case "Resources":
                    OnPropertyChanged("Resources");
                    break;
                case "Damage":
                    OnPropertyChanged("Damage");
                    break;
                case "Progress":
                    OnPropertyChanged("Progress");
                    break;
                default:
                    break;
            }
        }

        public byte Resources
        {
            get { return questInPlay.Resources; }
        }

        public byte Damage
        {
            get { return questInPlay.Damage; }
        }

        public byte Progress
        {
            get { return questInPlay.Progress; }
        }
    }
}
