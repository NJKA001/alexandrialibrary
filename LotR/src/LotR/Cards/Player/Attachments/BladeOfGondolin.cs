using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player.Heroes;
using LotR.Effects;
using LotR.Effects.Choices;
using LotR.Games;
using LotR.Effects.Phases;
using LotR.Effects.Phases.Any;
using LotR.Effects.Phases.Combat;

namespace LotR.Cards.Player.Attachments
{
    public class BladeOfGondolin
        : AttachmentCardBase
    {
        public BladeOfGondolin()
            : base("Blade of Gondolin", CardSet.Core, 39, Sphere.Tactics, 1, false, true)
        {
            AddTrait(Trait.Item);
            AddTrait(Trait.Weapon);

            AddEffect(new AddOneAttackWhenAttackingAnOrc(this));
            AddEffect(new AddOneProgressTokenAfterDefeatingAnEnemy(this));
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
                    var traitStep = new CheckForTraitStep(step.Phase, step.Player, step.Target, Trait.Orc);
                    step.Target.Card.CheckForTrait(traitStep);

                    if (traitStep.HasTrait)
                    {
                        step.Attack += 1;
                    }
                }
            }
        }

        public class AddOneProgressTokenAfterDefeatingAnEnemy
            : ResponseEffectBase, IAfterEnemyDefeated
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

            public override void Resolve(IPhaseStep step, IChoice choice)
            {
                step.AddProgressToCurrentQuest(1);
            }
        }
    }
}
