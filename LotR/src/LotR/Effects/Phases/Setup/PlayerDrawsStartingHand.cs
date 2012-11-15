using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Effects.Choices;
using LotR.Effects.Payments;
using LotR.States;

namespace LotR.Effects.Phases.Setup
{
    public class PlayerDrawsStartingHand
        : FrameworkEffectBase, IDuringSetup
    {
        public PlayerDrawsStartingHand(IGame game, IPlayer player)
            : base("Draw Setup Hand", GetDescription(player), game)
        {
            if (player == null)
                throw new ArgumentNullException("player");

            this.Player = player;
        }

        private static string GetDescription(IPlayer player)
        {
            return string.Format("{0} draws 6 cards from the top of their player deck.", player.Name);
        }

        public IPlayer Player
        {
            get;
            private set;
        }

        public void DuringSetup(IGame game)
        {
            Player.DrawCards(6);
        }

        public override IEffectOptions GetOptions(IGame game)
        {
            var startingHand = Player.Hand.Cards.ToList();
            var choice = new ChooseToKeepStartingHand("If a player does not wish to keep their starting hand, they may take a single mulligan, by shuffling those 6 cards back into their deck and drawing 6 new cards.", game, Player, startingHand);
            return new EffectOptions(choice);
        }

        public override string Resolve(IGame game, IEffectOptions options)
        {
            var chooseToKeep = options.Choice as IChooseToKeepStartingHand;
            if (chooseToKeep == null)
                return GetCancelledString();

            var player = chooseToKeep.Players.FirstOrDefault();
            if (player == null)
                return GetCancelledString();

            if (chooseToKeep.KeepStartingHand)
                return string.Format("Player {0} choose to keep their starting hand", player.Name);

            var startingHand = Player.Hand.Cards.ToList();
            Player.Hand.RemoveCards(startingHand);
            Player.Deck.ShuffleIn(startingHand);
            Player.DrawCards(6);

            var sb = new StringBuilder();
            sb.AppendFormat("Player {0} took a mulligan and received a new starting hand of\r\n", player.Name);
            foreach (var card in Player.Hand.Cards)
            {
                sb.AppendFormat("\r\n  {0} ({1})", card.Title, card.PrintedCardType);
            }

            return sb.ToString();
        }
    }
}
