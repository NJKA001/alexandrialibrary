using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public interface IPhaseStep
    {
        IPhase Phase { get; }
        IPlayer Player { get; }

        ICardInPlay GetCardInPlay(ICard card);

        IEnumerable<ICardEffect> Effects { get; }
        void AddEffect(ICardEffect effect);
    }
}
