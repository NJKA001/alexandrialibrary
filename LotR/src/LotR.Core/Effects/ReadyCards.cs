using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Effects
{
    public class ReadyCards
        : ReversableEffectBase, IReadyCards
    {
        public ReadyCards(IPhaseStep step, IEnumerable<IExhaustableCard> targets)
            : base("Ready cards")
        {
            this.step = step;
            this.Targets = targets;
        }

        private readonly IPhaseStep step;
        private readonly IDictionary<Guid, bool> wasReadied = new Dictionary<Guid, bool>();

        public IEnumerable<IExhaustableCard> Targets
        {
            get;
            private set;
        }

        public override void Execute()
        {
            foreach (var target in Targets)
            {
                var inPlay = step.GetCardInPlay(target.Id);
                if (inPlay == null)
                    continue;

                if (target.IsExhausted)
                {
                    target.Ready();
                    wasReadied[target.Id] = true;
                }
                else
                {
                    wasReadied[target.Id] = false;
                }
            }
        }

        public override void Undo()
        {
            foreach (var target in Targets)
            {
                var inPlay = step.GetCardInPlay(target.Id);
                if (inPlay == null)
                    continue;

                if (wasReadied[target.Id])
                    target.Exhaust();
            }
        }
    }
}
