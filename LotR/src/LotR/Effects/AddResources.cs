using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects.Phases;

namespace LotR.Effects
{
    public class AddResources
        : ReversableEffectBase, IAddResources
    {
        public AddResources(IPhaseStep step, IDictionary<Guid, byte> targetsAndAmounts)
            : base("Add Resources")
        {
            this.step = step;
            this.TargetsAndAmounts = targetsAndAmounts;
        }

        private readonly IPhaseStep step;

        public IDictionary<Guid, byte> TargetsAndAmounts
        {
            get;
            private set;
        }

        public override void Execute()
        {
            foreach (var pair in TargetsAndAmounts)
            {
                var target = step.GetCardInPlay(pair.Key);
                if (target == null)
                    continue;

                target.AddResources(pair.Value);
            }
        }

        public override void Undo()
        {
            foreach (var pair in TargetsAndAmounts)
            {
                var target = step.GetCardInPlay(pair.Key);
                if (target == null)
                    continue;

                target.RemoveResources(pair.Value);
            }
        }
    }
}
