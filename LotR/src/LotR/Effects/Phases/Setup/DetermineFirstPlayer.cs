using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects.Choices;
using LotR.Effects.Payments;
using LotR.States;

namespace LotR.Effects.Phases.Setup
{
    public class DetermineFirstPlayer
        : FrameworkEffectBase, IDuringSetup
    {
        public DetermineFirstPlayer(IGame game)
            : base("Determine First Player", "The players determine a FIRST PLAYER based on a majority group decision. If this proves impossible, determine a first player at random.", game)
        {
        }

        public void DuringSetup(IGame game)
        {
            if (game.Players.Count() == 1)
            {
                game.Players.First().IsFirstPlayer = true;
            }
        }

        public override IEffectHandle GetHandle(IGame game)
        {
            if (game.Players.Count() == 1)
                return new EffectHandle(this);

            var players = game.Players.ToList();
            var answers = new List<IAnswer>();
            foreach (var player in players)
            {
                answers.Add(new Answer<IGame, IPlayer>(player.Name, game, player, (source, item) => item.IsFirstPlayer = true));
            }

            var random = new Random();
            var randomPlayer = players[random.Next(0, players.Count - 1)];

            answers.Add(new Answer<IGame, IPlayer>("Determine a first player at random", game, randomPlayer, (source, item) => item.IsFirstPlayer = true));

            var question = new Question<IGame>("Who will be first player?", game, game.Players.First(), answers);
            var choice = new Choice<IGame>("The players determine a first player based on a majority group decision. If this proves impossible, determine a first player at random.", game, new List<IQuestion> { question });

            return new EffectHandle(this, choice); //new ChooseFirstPlayer(game));
        }

        public override void Trigger(IGame game, IEffectHandle handle)
        {
            if (game.Players.Count() == 1)
            {
                handle.Cancel("There is only player, this player will always be the first player");
                return;
            }

            var firstPlayerChoice = handle.Choice as IChooseFirstPlayer;
            if (firstPlayerChoice == null)
                throw new InvalidOperationException("choice is not a valid IFirstPlayerChoice");

            if (firstPlayerChoice.FirstPlayer == null)
                throw new InvalidOperationException("first player choice is undefined");

            firstPlayerChoice.FirstPlayer.IsFirstPlayer = true;

            foreach (var player in game.Players.Where(x => !x.IsFirstPlayer))
            {
                player.IsFirstPlayer = false;
            }

            handle.Resolve(string.Format("Determine First Player: {0}", game.FirstPlayer.Name));
        }
    }
}
