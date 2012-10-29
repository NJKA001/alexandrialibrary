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
            : base("Determine First Player", game)
        {
        }

        public void DuringSetup(IGame game)
        {
            if (game.Players.Count() == 1)
            {
                game.Players.First().IsFirstPlayer = true;
            }
        }

        public override IChoice GetChoice(IGame game)
        {
            if (game.Players.Count() == 1)
                return null;

            return new ChooseFirstPlayer(game);
        }

        public override void Resolve(IGame game, IPayment payment, IChoice choice)
        {
            if (game.Players.Count() == 1)
                return;

            var firstPlayerChoice = choice as IChooseFirstPlayer;
            if (firstPlayerChoice == null || firstPlayerChoice.FirstPlayer == null)
                return;

            firstPlayerChoice.FirstPlayer.IsFirstPlayer = true;
        }

        public override string GetResolutionDescription(IGame game, IPayment payment, IChoice choice)
        {
            return string.Format("Determine First Player: {0}", game.FirstPlayer.Name);
        }
    }
}
