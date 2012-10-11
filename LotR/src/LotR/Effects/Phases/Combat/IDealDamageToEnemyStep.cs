using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Encounter.Enemies;
using LotR.States;

namespace LotR.Effects.Phases.Combat
{
    public interface IDealDamageToEnemyStep
    {
        ICardInPlay<IEnemyCard> Target { get; set; }
        byte Damage { get; set; }
    }
}
