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

        private void KeepStartingHand(IEffectHandle handle)
        {
            handle.Resolve(string.Format("{0} chose to keep their starting hand", Player.Name));
        }

        private void DrawNewStartingHand(IEffectHandle handle)
        {
            var startingHand = Player.Hand.Cards.ToList();
            Player.Hand.RemoveCards(startingHand);
            Player.Deck.ShuffleIn(startingHand);
            Player.DrawCards(6);

            handle.Resolve(string.Format("{0} chose to take a mulligan and draw a new starting hand", Player.Name));
        }

        public override IEffectHandle GetHandle(IGame game)
        {
            var startingHand = Player.Hand.Cards.ToList();
            //var choice = new ChooseToKeepStartingHand("If a player does not wish to keep their starting hand, they may take a single mulligan, by shuffling those 6 cards back into their deck and drawing 6 new cards.", game, Player, startingHand);

            var answers = new List<IAnswer>();
            answers.Add(new Answer<IGame, bool>("Yes, I want to keep my starting hand", game, true, (source, handle, item) => KeepStartingHand(handle)));
            answers.Add(new Answer<IGame, bool>("No, I will take a mulligan and draw a new starting hand", game, false, (source, handle, item) => DrawNewStartingHand(handle)));

            var text = string.Format("{0}, do you want to keep your starting hand?", Player.Name);
            var question = new Question<IGame>(text, game, Player, answers);

            var choice = new Choice<IGame>("If a player does not wish to keep their starting hand, they may take a single mulligan, by suffling those 6 cards back into their deck and drawing 6 new cards.", game, question);

            return new EffectHandle(this, choice);
        }

        /*
        public override void Trigger(IGame game, IEffectHandle handle)
        {
            //var kept = handle.Choice.Question.Answers.First();

            //if (kept.IsChosen)
            //{
            //    kept.Execute(game, handle);
            //    handle.Resolve(string.Format("{0} chose to keep their starting hand", Player.Name));
            //}
            //else
            //{
            //    var mulligan = handle.Choice.Question.Answers.Last();
            //    mulligan.Execute(game, handle);
            //    handle.Resolve(string.Format("{0} chose to take a mulligan and draw a new starting hand", Player.Name));
            //}


            //var chooseToKeep = handle.Choice as IChooseToKeepStartingHand;
            //if (chooseToKeep == null)
            //    { handle.Cancel(GetCancelledString()); return; }

            //var player = chooseToKeep.Players.FirstOrDefault();
            //if (player == null)
            //    { handle.Cancel(GetCancelledString()); return; }

            //if (chooseToKeep.KeepStartingHand)
            //{
            //    handle.Resolve(string.Format("{0} choose to keep their starting hand", player.Name));
            //}

            //var startingHand = Player.Hand.Cards.ToList();
            //Player.Hand.RemoveCards(startingHand);
            //Player.Deck.ShuffleIn(startingHand);
            //Player.DrawCards(6);

            //handle.Resolve(string.Format("{0} took a mulligan and received a new starting hand", player.Name));
        }
        */
    }
}
