using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        private void ChooseFirstPlayer(IEffectHandle handle, IPlayer player)
        {
            player.IsFirstPlayer = true;

            handle.Resolve(string.Format("{0} was chosen as the first player", player.Name));
        }

        private void ChooseFirstPlayerRandomly(IEffectHandle handle, IPlayer player)
        {
            player.IsFirstPlayer = true;

            handle.Resolve(string.Format("{0} was chosen randomly as the first player", player.Name));
        }

        public override IEffectHandle GetHandle(IGame game)
        {
            var builder =
                new ChoiceBuilder<IGame>("The players determine a first player based on a majority group decision. If this proves impossible, determine a first player at random.", game, game.Players.First())
                    .Question("Who will be first player?")
                        .Answers(game.Players, (player) => player.Name, (source, handle, item) => ChooseFirstPlayer(handle, item))
                        .Answer("Determine a first player at random", game.Players.GetRandomItem(), (source, handle, item) => ChooseFirstPlayerRandomly(handle, item));

            return new EffectHandle(this, builder.ToChoice());
        }

        //public override void Trigger(IGame game, IEffectHandle handle)
        //{
        //    if (game.Players.Count() == 1)
        //    {
        //        handle.Cancel("There is only player, this player will always be the first player");
        //        return;
        //    }

        //    var firstPlayerChoice = handle.Choice as IChooseFirstPlayer;
        //    if (firstPlayerChoice == null)
        //        throw new InvalidOperationException("choice is not a valid IFirstPlayerChoice");

        //    if (firstPlayerChoice.FirstPlayer == null)
        //        throw new InvalidOperationException("first player choice is undefined");

        //    firstPlayerChoice.FirstPlayer.IsFirstPlayer = true;

        //    foreach (var player in game.Players.Where(x => !x.IsFirstPlayer))
        //    {
        //        player.IsFirstPlayer = false;
        //    }

        //    handle.Resolve(string.Format("Determine First Player: {0}", game.FirstPlayer.Name));
        //}
    }
}
