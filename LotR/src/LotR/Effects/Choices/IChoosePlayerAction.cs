using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;
using LotR.Effects;
using LotR.States;

namespace LotR.Effects.Choices
{
    public interface IChoosePlayerAction
        : IChoice
    {
        bool IsTakingAction { get; set; }
        IPlayerCard CardToPlay { get; set; }
        ICardEffect CardEffectToTrigger { get; set; }
    }
}
