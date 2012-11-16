using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;
using LotR.Effects.Choices;
using LotR.Effects.Payments;
using LotR.States;

namespace LotR.Effects.Phases.Any
{
    public class PlayerActionWindow
        : FrameworkEffectBase
    {
        public PlayerActionWindow(IGame game, IPlayer player)
            : base("Player Action Window", GetDescription(player), game)
        {
            this.player = player;
        }

        private static string GetDescription(IPlayer player)
        {
            return string.Format("{0} has the opportunity to play cards or trigger effects", player.Name);
        }

        private readonly IPlayer player;

        public override IEffectHandle GetHandle(IGame game)
        {
            return new EffectHandle(new ChoosePlayerAction(game, player));
        }

        public override void Resolve(IGame game, IEffectHandle handle)
        {
            var actionChoice = handle.Choice as IChoosePlayerAction;
            if (actionChoice == null)
                { handle.Cancel(GetCancelledString()); return; }

            if (actionChoice.CardToPlay != null && actionChoice.CardToPlay is ICostlyCard)
            {
                var costlyCard = actionChoice.CardToPlay as ICostlyCard;
                var playCardEffect = new PlayCardFromHandEffect(game, costlyCard);
                game.AddEffect(playCardEffect);
                var playCardOptions = game.GetHandle(playCardEffect);
                game.TriggerEffect(playCardEffect, playCardOptions);
            }
            else if (actionChoice.CardEffectToTrigger != null)
            {
                game.AddEffect(actionChoice.CardEffectToTrigger);
                var playEffectOptions = game.GetHandle(actionChoice.CardEffectToTrigger);
                game.TriggerEffect(actionChoice.CardEffectToTrigger, playEffectOptions);
            }

            handle.Resolve(GetCompletedStatus());
        }
    }
}
