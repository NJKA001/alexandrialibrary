using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;
using LotR.Effects.Phases;
using LotR.States;

namespace LotR.Effects
{
    public class ReadyCards
        : ReversableEffectBase, IReadyCards
    {
        public ReadyCards(IPhaseStep step, IEnumerable<ICardInPlay<IExhaustableCard>> targets)
            : base("Ready cards")
        {
            this.step = step;
            this.Targets = targets;
        }

        private readonly IPhaseStep step;
        private readonly IDictionary<Guid, bool> wasReadied = new Dictionary<Guid, bool>();

        public IEnumerable<ICardInPlay<IExhaustableCard>> Targets
        {
            get;
            private set;
        }

        public override void Execute()
        {
            foreach (var target in Targets)
            {
                //var inPlay = step.GetCardInPlay(target.CardId);
                //if (inPlay == null)
                //    continue;

                //if (target.IsExhausted)
                //{
                //    target.Ready();
                //    wasReadied[target.CardId] = true;
                //}
                //else
                //{
                //    wasReadied[target.CardId] = false;
                //}
            }
        }

        public override void Undo()
        {
            foreach (var target in Targets)
            {
                //var inPlay = step.GetCardInPlay(target.CardId);
                //if (inPlay == null)
                //    continue;

                //if (wasReadied[target.CardId])
                //    target.Exhaust();
            }
        }
    }
}
