using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.Cards.Encounter.Objectives
{
    public interface IObjectiveAllyCard
        : IObjectiveCard, IDamageableCard, IAttackingCard, IDefendingCard, IWillpowerfulCard
    {
    }
}
