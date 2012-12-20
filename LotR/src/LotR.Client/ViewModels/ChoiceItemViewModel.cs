using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Threading;

using LotR.Cards;
using LotR.Cards.Encounter;
using LotR.Cards.Player;
using LotR.Effects;
using LotR.States;

namespace LotR.Client.ViewModels
{
    public class ChoiceItemViewModel
        : ViewModelBase
    {
        public ChoiceItemViewModel(Dispatcher dispatcher, IChoiceItem item)
            : base(dispatcher)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            this.item = item;
            if (item is IChoice)
            {
                choice = item as IChoice;

                var child = new ChoiceItemViewModel(dispatcher, choice.Question);
                child.Parent = this;
                children.Add(child);
            }
            else if (item is IQuestion)
            {
                question = item as IQuestion;

                foreach (var answer in question.Answers)
                {
                    var child = new ChoiceItemViewModel(dispatcher, answer);
                    child.Parent = this;
                    children.Add(child);
                }
            }
            else if (item is IAnswer)
            {
                answer = item as IAnswer;

                if (answer.FollowUp != null)
                {
                    var child = new ChoiceItemViewModel(dispatcher, answer.FollowUp);
                    child.Parent = this;
                    children.Add(child);
                }

                if (answer.ItemType is IPlayerCard)
                {
                    var playerCard = answer.GetObject<IPlayerCard>();
                    card = playerCard;
                    playerCardViewModel = new PlayerCardViewModel(dispatcher, playerCard);
                    cardVisibility = Visibility.Visible;
                }
                else if (answer.ItemType is IEncounterCard)
                {
                    var encounterCard = answer.GetObject<IEncounterCard>();
                    card = encounterCard;
                    encounterCardViewModel = new EncounterCardViewModel(dispatcher, encounterCard);
                    cardVisibility = Visibility.Visible;
                }
                else if (answer.ItemType is ICardInPlay)
                {
                    var cardInPlay = answer.GetObject<ICardInPlay>();
                    card = cardInPlay.BaseCard;
                    if (cardInPlay.BaseCard is IPlayerCard)
                    {

                    }
                }
            }
        }

        private readonly IChoiceItem item;
        private readonly IChoice choice;
        private readonly IQuestion question;
        private readonly IAnswer answer;
        private readonly ICard card;

        private readonly PlayerCardViewModel playerCardViewModel;
        private readonly EncounterCardViewModel encounterCardViewModel;
        private readonly ObservableCollection<ChoiceItemViewModel> children = new ObservableCollection<ChoiceItemViewModel>();

        private Visibility cardVisibility = Visibility.Collapsed;
        private Visibility cardInPlayVisibility = Visibility.Collapsed;
        private bool isChosen;

        private void ChoiceItemPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            switch (args.PropertyName)
            {
                case "IsChosen":
                    OnPropertyChanged("IsChosen");
                    break;
                case "IsExpanded":
                    OnPropertyChanged("IsExpanded");
                    break;
                default:
                    break;
            }
        }

        public IEnumerable<ChoiceItemViewModel> Children
        {
            get { return children; }
        }

        public Visibility CardVisibility
        {
            get { return cardVisibility; }
        }

        public Visibility CardInPlayVisibility
        {
            get { return cardInPlayVisibility; }
        }

        public Visibility ChoosableVisibility
        {
            get
            {
                return answer != null ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public uint MinimumChosenAnswers
        {
            get { return question != null ? question.MinimumChosenAnswers : 0; }
        }

        public uint MaximumChosenAnswers
        {
            get { return question != null ? question.MaximumChosenAnswers : 0; }
        }

        public bool IsChoice { get { return choice != null; } }
        public bool IsQuestion { get { return question != null; } }
        public bool IsAnswer { get { return answer != null; } }

        public bool IsChosen
        {
            get { return answer != null ? answer.IsChosen : false; }
            set
            {
                isChosen = value;
                if (answer != null)
                {
                    answer.IsChosen = value;
                }

                OnPropertyChanged("IsChosen");
            }
        }

        public bool IsExpanded
        {
            get { return item.IsExpanded; }
            set { item.IsExpanded = value; }
        }

        public ChoiceItemViewModel Parent
        {
            get;
            protected set;
        }

        public Guid ItemId
        {
            get { return item.ItemId; }
        }

        public string Text
        {
            get { return item.Text; }
        }

        public string CardTitle
        {
            get { return card != null ? card.Title : string.Empty; }
        }

        public string CardText
        {
            get { return card != null ? card.Text.ToString() : string.Empty; }
        }

        public object ImageSource
        {
            get
            {
                if (playerCardViewModel != null)
                {
                    return playerCardViewModel.ImageSource;
                }
                else if (encounterCardViewModel != null)
                {
                    return encounterCardViewModel.ImageSource;
                }

                return null;
            }
        }

        public bool IsValid(IGame game)
        {
            if (choice == null)
                return true;

            return choice.IsValid(game);
        }
    }
}
