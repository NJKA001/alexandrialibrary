using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects.Choices;
using LotR.Effects.Payments;
using LotR.States;

namespace LotR.Effects.Phases.Any
{
    public class PlayerActionWindow
        : FrameworkEffectBase
    {
        public PlayerActionWindow(IGame game, IPlayer player)
            : base(string.Empty, game)
        {
            this.player = player;
        }

        private readonly IPlayer player;

        public override IChoice GetChoice(IGame game)
        {
            return new ChoosePlayerAction(game, player);
        }

        public override void Resolve(IGame game, IPayment payment, IChoice choice)
        {
            var actionChoice = choice as IChoosePlayerAction;
            if (actionChoice == null)
                return;
        }
    }
}
