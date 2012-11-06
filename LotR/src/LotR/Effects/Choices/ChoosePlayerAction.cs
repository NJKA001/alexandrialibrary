using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;
using LotR.States;

namespace LotR.Effects.Choices
{
    public class ChoosePlayerAction
        : ChoiceBase, IChoosePlayerAction
    {
        public ChoosePlayerAction(IGame game, IPlayer player)
            : base(GetDescription(game, player), game, player)
        {
        }

        private static string GetDescription(IGame game, IPlayer player)
        {
            return string.Format("{0} can choose to take an action during the {1} step of the {2} phase. They may play a card from their hand, trigger an effect on a card in play or pass on taking any actions.", player.Name, game.CurrentPhase.StepName, game.CurrentPhase.Name);
        }

        public IPlayerCard CardToPlay { get; set; }

        public ICardEffect CardEffectToTrigger { get; set; }
    }
}
