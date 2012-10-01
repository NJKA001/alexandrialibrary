using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Core.Effects;
using LotR.Core.Phases.Any;
using LotR.Core.Phases.Combat;

namespace LotR.Core.Attachments
{
    public class BladeOfGondolin
        : AttachmentCardBase
    {
        public BladeOfGondolin()
            : base("Blade of Gondolin", SetNames.Core, 39, Sphere.Tactics, 1, false, true)
        {
            Trait(Traits.Item);
            Trait(Traits.Weapon);

            Effect(new AddOneAttackWhenAttackingAnOrc(this));
            Effect(new AddOneProgressTokenAfterDefeatingAnEnemy(this));
        }

        public override bool CanBeAttachedTo(IPhaseStep step, ICardInPlay cardInPlay)
        {
            if (cardInPlay == null)
                throw new ArgumentNullException("cardInPlay");

            return (cardInPlay.Card is IHeroCard);
        }

        public class AddOneAttackWhenAttackingAnOrc
            : PassiveEffect, IDetermineAttack
        {
            public AddOneAttackWhenAttackingAnOrc(BladeOfGondolin source)
                : base("Attached hero gets +1 Attack when attacking an Orc.", source)
            {
            }

            public void DetermineAttack(IDetermineAttackStep step)
            {
                if (step.Target != null)
                {
                    var traitStep = new CheckForTraitStep(step.Phase, step.Player, step.Target, Traits.Orc);
                    step.Target.Card.CheckForTrait(traitStep);

                    if (traitStep.HasTrait)
                    {
                        step.Attack += 1;
                    }
                }
            }
        }

        public class AddOneProgressTokenAfterDefeatingAnEnemy
            : ResponseEffect, IAfterEnemyDefeated
        {
            public AddOneProgressTokenAfterDefeatingAnEnemy(BladeOfGondolin source)
                : base("After attached hero attacks and destroys an enemy, place 1 progress token on the current quest", source)
            {
            }

            public void AfterEnemyDefeated(IEnemyDefeatedStep step)
            {
                var attachment = step.GetCardInPlay(Source.Id) as IAttachmentInPlay;
                if (attachment == null || attachment.AttachedTo == null)
                    return;

                var hero = attachment.AttachedTo as IHeroCard;
                if (hero == null)
                    return;

                if (step.Attackers.Any(x => x.Id == hero.Id))
                {
                    step.AddEffect(this);
                }
            }

            public override void Resolve(IPhaseStep step, IPayment payment)
            {
                step.AddProgressToCurrentQuest(1);
            }
        }
    }
}
