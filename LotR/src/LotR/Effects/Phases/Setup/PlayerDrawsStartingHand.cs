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
            : base(GetDescription(player), game)
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

        public override IChoice GetChoice(IGame game)
        {
            var startingHand = Player.Hand.Cards.ToList();
            return new ChooseToKeepStartingHand("If a player does not wish to keep their starting hand, they may take a single mulligan, by shuffling those 6 cards back into their deck and drawing 6 new cards.", game, Player, startingHand);
        }

        public override void Resolve(IGame game, IPayment payment, IChoice choice)
        {
            var chooseToKeep = choice as IChooseToKeepStartingHand;
            if (chooseToKeep == null || chooseToKeep.KeepStartingHand)
                return;

            var startingHand = Player.Hand.Cards.ToList();
            Player.Hand.RemoveCards(startingHand);
            Player.Deck.ShuffleIn(startingHand);
            Player.DrawCards(6);
        }

        public override string GetResolutionDescription(IGame game, IPayment payment, IChoice choice)
        {
            var chooseToKeep = choice as IChooseToKeepStartingHand;
            if (chooseToKeep == null)
                return ToString();

            var playerName = chooseToKeep.Players.First().Name;

            if (chooseToKeep.KeepStartingHand)
                return string.Format("Player {0} choose to keep their starting hand", playerName);

            var sb = new StringBuilder();
            sb.AppendFormat("Player {0} took a mulligan and received a new starting hand of\r\n", playerName);
            foreach (var card in Player.Hand.Cards)
            {
                sb.AppendFormat("\r\n  {0} ({1})", card.Title, card.PrintedCardType);
            }

            return sb.ToString();
        }
    }
}
