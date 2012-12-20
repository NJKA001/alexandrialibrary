using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Threading;

using LotR.Cards;
using LotR.Cards.Player;
using LotR.Cards.Player.Allies;
using LotR.Cards.Player.Attachments;
using LotR.Cards.Player.Heroes;
using LotR.Cards.Player.Treasures;
using LotR.States;

namespace LotR.Client.ViewModels
{
    public class PlayerViewModel
        : ViewModelBase
    {
        public PlayerViewModel(Dispatcher dispatcher, IGame game, IPlayer player)
            : base(dispatcher)
        {
            if (game == null)
                throw new ArgumentNullException("game");
            if (player == null)
                throw new ArgumentNullException("player");

            this.player = player;
            player.PropertyChanged += (sender, args) => PlayerPropertyChanged(sender, args);
            
            player.Hand.RegisterCardAddedCallback(x => CardAddedToHand(x));
            player.Hand.RegisterCardRemovedCallback(x => CardRemovedFromHand(x));
            player.RegisterCardAddedToPlayCallback(x => CardAddedToPlay(x));
            player.RegisterCardRemovedFromPlayCallback(x => CardRemovedFromPlay(x));
        }

        private readonly IPlayer player;
        private readonly ObservableCollection<PlayerCardInPlayViewModel<IHeroCard>> heroes = new ObservableCollection<PlayerCardInPlayViewModel<IHeroCard>>();
        private readonly ObservableCollection<PlayerCardInPlayViewModel> cardsInPlay = new ObservableCollection<PlayerCardInPlayViewModel>();
        private readonly ObservableCollection<PlayerCardViewModel> hand = new ObservableCollection<PlayerCardViewModel>();

        private void PlayerPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            switch (args.PropertyName)
            {
                case "CurrentThreat":
                    OnPropertyChanged("CurrentThreat");
                    break;
                case "IsFirstPlayer":
                    OnPropertyChanged("IsFirstPlayer");
                    OnPropertyChanged("FirstPlayerVisibility");
                    break;
                case "IsActivePlayer":
                    OnPropertyChanged("IsActivePlayer");
                    OnPropertyChanged("ActivePlayerVisibility");
                    break;
                default:
                    break;
            }
        }

        private void CardAddedToHand(IPlayerCard card)
        {
            Dispatch(() => hand.Add(new PlayerCardViewModel(dispatcher, card)));
        }

        private void CardRemovedFromHand(IPlayerCard card)
        {
            var viewModel = hand.Where(x => x.CardId == card.Id).FirstOrDefault();
            if (viewModel == null)
                return;

            Dispatch(() => hand.Remove(viewModel));
        }

        private void CardAddedToPlay(ICardInPlay cardInPlay)
        {
            var heroInPlay = cardInPlay as IHeroInPlay;
            if (heroInPlay != null)
            {
                var heroViewModel = new PlayerCardInPlayViewModel<IHeroCard>(dispatcher, heroInPlay);
                Dispatch(() => heroes.Add(heroViewModel));
                return;
            }

            PlayerCardInPlayViewModel viewModel = null;

            var allyInPlay = cardInPlay as IAllyInPlay;
            if (allyInPlay != null)
            {
                viewModel = new PlayerCardInPlayViewModel<IAllyCard>(dispatcher, allyInPlay);
            }

            var attachmentInPlay = cardInPlay as IAttachmentInPlay;
            if (attachmentInPlay != null)
            {
                viewModel = new PlayerCardInPlayViewModel<IAttachmentCard>(dispatcher, attachmentInPlay);
            }

            var treasureInPlay = cardInPlay as ITreasureInPlay;
            if (treasureInPlay != null)
            {
                viewModel = new PlayerCardInPlayViewModel<ITreasureCard>(dispatcher, treasureInPlay);
            }

            if (viewModel == null)
                return;

            Dispatch(() => cardsInPlay.Add(viewModel));
        }

        private void CardRemovedFromPlay(ICardInPlay cardInPlay)
        {
            var viewModel = cardsInPlay.Where(x => x.CardId == cardInPlay.BaseCard.Id).FirstOrDefault();
            if (viewModel == null)
                return;

            Dispatch(() => cardsInPlay.Remove(viewModel));
        }

        public string PlayerName
        {
            get { return player.Name; }
        }

        public byte CurrentThreat
        {
            get { return player.CurrentThreat; }
        }

        public bool IsFirstPlayer
        {
            get { return player.IsFirstPlayer; }
        }

        public Visibility FirstPlayerVisibility
        {
            get { return player.IsFirstPlayer ? Visibility.Visible : Visibility.Collapsed; }
        }

        public bool IsActivePlayer
        {
            get { return player.IsActivePlayer; }
        }

        public Visibility ActivePlayerVisibility
        {
            get { return player.IsActivePlayer ? Visibility.Visible : Visibility.Collapsed; }
        }

        public IEnumerable<PlayerCardInPlayViewModel<IHeroCard>> Heroes
        {
            get { return heroes; }
        }

        public IEnumerable<PlayerCardViewModel> Hand
        {
            get { return hand; }
        }

        public IEnumerable<PlayerCardInPlayViewModel> CardsInPlay
        {
            get { return cardsInPlay; }
        }
    }
}
