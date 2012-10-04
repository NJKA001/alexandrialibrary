using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Core.Phases.Any;

namespace LotR.Core
{
    public interface ICharacterCard
        : IPlayerCard, 
        IWillpowerfulCard,
        IAttackingCard, 
        IDefendingCard, 
        IDamageableCard
    {
    }
}
