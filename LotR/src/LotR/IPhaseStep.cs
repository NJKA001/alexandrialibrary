using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR
{
    public interface IPhaseStep
    {
        IPhase Phase { get; }
        IPlayer Player { get; }

        ICard GetCard(Guid id);
        ICardInPlay GetCardInPlay(Guid id);
        bool CardIsInPlay(Guid id);
        IPlayer GetController(Guid id);
        IPlayer GetOwner(Guid id);

        IEnumerable<IEffect> Effects { get; }
        void AddEffect(IEffect effect);

        void AddProgressToCurrentQuest(byte value);
        void RemoveProgressFromCurrentQuest(byte value);
    }
}
