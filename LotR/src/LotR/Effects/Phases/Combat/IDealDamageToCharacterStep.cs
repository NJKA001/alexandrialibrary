using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.States;

namespace LotR.Effects.Phases.Combat
{
    public interface IDealDamageToCharacterStep
    {
        ICardInPlay<IDamageableCard> Target { get; }
        byte Damage { get; }
    }
}
