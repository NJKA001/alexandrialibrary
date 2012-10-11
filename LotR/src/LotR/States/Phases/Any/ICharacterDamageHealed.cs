using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;

namespace LotR.States.Phases.Any
{
    public interface ICharacterDamageHealed
        : IState
    {
        ICardInPlay<ICharacterCard> Character { get; }
        byte DamageHealed { get; }
    }
}
