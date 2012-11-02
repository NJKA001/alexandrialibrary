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
            : base(player.Name + " can choose to take an action.\r\nThey may play a card from their hand or trigger an effect on a card in play", game, player)
        {
        }

        public IPlayerCard CardToPlay { get; set; }

        public ICardEffect CardEffectToTrigger { get; set; }
    }
}
