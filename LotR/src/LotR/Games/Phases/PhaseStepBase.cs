using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Effects;

namespace LotR.Games.Phases
{
    public abstract class PhaseStepBase
        : IPhaseStep
    {
        protected PhaseStepBase(IPhase phase, IPlayer player)
        {
            this.Phase = phase;
            this.Player = player;
        }

        private readonly IList<IEffect> effects = new List<IEffect>();

        public IPhase Phase
        {
            get;
            private set;
        }

        public IPlayer Player
        {
            get;
            private set;
        }

        public ICard GetCard(Guid id)
        {
            return Phase.Round.Game.GetCard(id);
        }

        public ICardInPlay GetCardInPlay(Guid id)
        {
            return Phase.Round.Game.GetCardInPlay(id);
        }

        public IPlayer GetController(Guid id)
        {
            return Phase.Round.Game.GetController(id);
        }

        public IPlayer GetOwner(Guid id)
        {
            return Phase.Round.Game.GetOwner(id);
        }

        public bool CardIsInPlay(Guid id)
        {
            return (Phase.Round.Game.GetCardInPlay(id) != null);
        }

        public IEnumerable<IEffect> Effects
        {
            get { return effects; }
        }

        public void AddEffect(IEffect effect)
        {
            if (effect == null)
                throw new ArgumentNullException("effect");

            effects.Add(effect);
        }

        public void AddStep(IPhaseStep step)
        {
            //TODO: Determine implementation
        }

        public void AddProgressToCurrentQuest(byte value)
        {
            Phase.Round.Game.ActiveQuest.AddProgressTokens(value);
        }

        public void RemoveProgressFromCurrentQuest(byte value)
        {
            Phase.Round.Game.ActiveQuest.RemoveProgressTokens(value);
        }
    }
}
