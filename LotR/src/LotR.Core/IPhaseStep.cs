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

        ICard GetCard(Guid id);
        ICardInPlay GetCardInPlay(Guid id);

        IEnumerable<IEffect> Effects { get; }
        void AddEffect(IEffect effect);
    }
}
